using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using WebApplication1.DB;
using WebApplication1.Health;
using WebApplication1.Interfaces;
using WebApplication1.Services;
using static WebApplication1.Services.JWTService;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Serilog;
using Serilog.Exceptions;
using WebApplication1.Hubs;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Extensions.Hosting;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .Enrich.WithExceptionDetails()
    .WriteTo.Email(from: "from@example.com", to: "to@example.com", host: "smtp.example.com")
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();

Log.Debug("Debug message");
Log.Information("Information message");
Log.Warning("Warning message");
Log.Error("Error message");
Log.Fatal("Fatal message");
Log.CloseAndFlush();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IStudentService, StudentService>(); // Для використання DI
builder.Services.AddScoped<IGroupService, GroupService>(); // Для використання DI
builder.Services.AddScoped<ISubjectService, SubjectService>(); // Для використання DI
builder.Services.AddScoped<IAuthService, AuthService>(); // Для використання DI
builder.Services.AddScoped<IVersionedService, VersionedService>(); // Для використання DI
builder.Services.AddScoped<ICurrencyService, CurrencyService>(); // Для використання DI
builder.Services.AddSingleton<IDB, DB>(); // Для використання єдиного екземпляру класа
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = AuthOptions.ISSUER,
        ValidateAudience = true,
        ValidAudience = AuthOptions.AUDIENCE,
        ValidateLifetime = true,
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true,
    };

});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
});

builder.Services.AddSwaggerGen(c =>
{   
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API V1", Version = "1.0" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "API V2", Version = "1.0" });
    c.SwaggerDoc("v3", new OpenApiInfo { Title = "API V3", Version = "1.0" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddSingleton<IHealthCheckResponseWriter, HealthWritter>();
builder.Services.AddHealthChecksUI()
    .AddInMemoryStorage();

builder.Services.AddHealthChecks()
    //.AddCheck("sample_health_check", () => HealthCheckResult.Healthy("Sample check is healthy."))
    .AddCheck<Service1HealthCheck>("service1")
    /*.AddSqlServer(configuration["ConnectionString"], name: "db")*/;

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddSerilog();
});

builder.Services.AddSignalR();

builder.Logging.AddOpenTelemetry(options =>
{
    options.SetResourceBuilder(ResourceBuilder.CreateDefault()
        .AddService("role-dice"))
        .AddOtlpExporter(opt =>
        {
            opt.Endpoint = new Uri("http://localhost:4318");
        })
        .AddConsoleExporter();
});

builder.Services.AddOpenTelemetry()
      .ConfigureResource(resource => resource.AddService("role-dice"))
      .WithTracing(tracing => tracing
          .AddAspNetCoreInstrumentation()
          .AddConsoleExporter()
          .SetSampler(new TraceIdRatioBasedSampler(0.1)))
      .WithMetrics(metrics => metrics
          .AddAspNetCoreInstrumentation()
          .AddConsoleExporter());

var app = builder.Build();



app.MapHealthChecks("/healthcheck");
app.UseHealthChecks("/healthcheck", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHealthChecksUI(options =>
{
    options.UIPath = "/healthchecks-ui";
    options.ApiPath = "/health-ui-api";
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<CurrencyHub>("/currencyhub");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "V2");
    c.SwaggerEndpoint("/swagger/v3/swagger.json", "V3");
});

app.UseStaticFiles();

app.Run();
