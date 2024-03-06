
using AnimeStockWebProject.Extensions;
using AnimeStockWebProject.Infrastructure.Data;
using AnimeStockWebProject.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AnimeStockDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options =>
{
    options.Password.RequireDigit = true;
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
})
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AnimeStockDbContext>();

builder.Services.AddMemoryCache();


builder.Services.AddControllersWithViews();

builder.Services.AddServices();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Home/Index";
    options.ExpireTimeSpan = TimeSpan.FromDays(3);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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

app.SeedAdministrator("ee8ddd02-ce94-4f77-8608-819b08dbbb32");

app.UseEndpoints(config =>
{
    config.MapControllerRoute(
        name: "areas",
        pattern: "/{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    config.MapControllerRoute(
        name: "default",
     pattern: "{controller=Home}/{action=Index}/{id?}");


    config.MapDefaultControllerRoute();

    config.MapRazorPages();
});

app.Run();
