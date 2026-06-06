using BLL;
using BLL.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using BLL.DTOs.StaffUser;
using BLL.DTOs.User;
using BLL.Services.Interfaces;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

// Cookie authentication for staff users
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = true;
    });

// lấy connection string từ appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add business logic layer services
builder.Services.AddBusinessLogicLayer(connectionString!);
builder.Services.Configure<VnpaySettings>(builder.Configuration.GetSection("VNPAY"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Seed staff user if not exists (for testing)
using (var scope = app.Services.CreateScope())
{
    var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
    var existing = userService.GetByEmailAsync("staff@local").GetAwaiter().GetResult();
    if (existing == null)
    {
        var createDto = new UserCreateDto
        {
            FullName = "Staff User",
            Email = "staff@local",
            PhoneNumber = "0123456789",
            Password = "P@ssw0rd",
            Role = 2 // Staff
        };
        userService.CreateAsync(createDto).GetAwaiter().GetResult();
    }
}

app.Run();
