using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using WebApplication1.DB;
using WebApplication1.Health;
using WebApplication1.Interfaces;
using WebApplication1.Services;
using static WebApplication1.Services.JWTService;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IStudentService, StudentService>(); // Для використання DI
builder.Services.AddScoped<IGroupService, GroupService>(); // Для використання DI
builder.Services.AddScoped<ISubjectService, SubjectService>(); // Для використання DI
builder.Services.AddScoped<IAuthService, AuthService>(); // Для використання DI
builder.Services.AddScoped<IVersionedService, VersionedService>(); // Для використання DI
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
    .AddCheck("sample_health_check", () => HealthCheckResult.Healthy("Sample check is healthy."))
    .AddCheck<Service1HealthCheck>("service1")
    .AddSqlServer(configuration["ConnectionString"], name: "db");

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
app.Run();
