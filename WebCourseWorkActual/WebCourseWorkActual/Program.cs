using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebCourseWorkActual;
using WebCourseWorkActual.DAL;
using WebCourseWorkActual.DAL.Interfaces;
using WebCourseWorkActual.DAL.Repositories;
using WebCourseWorkActual.Domain.Entity;
using WebCourseWorkActual.Service.Implementations;
using WebCourseWorkActual.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connection));

//builder.Services.AddScoped<IUserService, UserService>();
//builder.Entity<User>(entry =>
//{
//    entry.ToTable(tb => tb.HasTrigger("MyTable_Insert"));
//});

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connection));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connection), ServiceLifetime.Scoped);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
    });

//builder.Services.AddScoped<IAccountService, AccountService>();
//builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.InitializeRepositories();
builder.Services.InitializeServices();

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


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
