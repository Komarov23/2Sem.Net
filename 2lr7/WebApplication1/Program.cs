using WebApplication1.DB;
using WebApplication1.Interfaces;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IStudentService, StudentService>(); // ��� ������������ DI
builder.Services.AddScoped<IGroupService, GroupService>(); // ��� ������������ DI
builder.Services.AddScoped<ISubjectService, SubjectService>(); // ��� ������������ DI
builder.Services.AddSingleton<IDB, DB>(); // ��� ������������ ������� ���������� �����

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
