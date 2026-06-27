using Admin.Blazor.Components;
using Admin.Blazor.Components.Auth;
using BLL;
using BLL.Settings;
using BLL.Services.Interfaces;
using BLL.Services;
using DAL.Repositories.Interfaces;
using DAL.Repositories;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// lấy connection string từ appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddRazorPages();

builder.Services.AddBusinessLogicLayer(connectionString!);
builder.Services.Configure<VnpaySettings>(builder.Configuration.GetSection("VNPAY"));


// Add authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/auth/login";
        options.LogoutPath = "/auth/logout";
    });
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<StaffAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<StaffAuthenticationStateProvider>());

var app = builder.Build();

// Seed admin staff account
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DAL.Data.AppDbContext>();
    db.Database.Migrate();
    
    if (!db.Staffs.Any(s => s.Email == "admin@electronexus.com"))
    {
        var adminPassword = BCrypt.Net.BCrypt.HashPassword("Admin@123");
        var admin = new DAL.Models.Staff
        {
            FullName = "Administrator",
            Email = "admin@electronexus.com",
            PasswordHash = adminPassword,
            Role = DAL.Enums.RoleEnum.Admin,
            IsActive = true,
            IsDeleted = false,
            CreatedAt = DateTime.UtcNow
        };
        db.Staffs.Add(admin);
        db.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

var sharedUploadsFolder = Path.GetFullPath(Path.Combine(app.Environment.ContentRootPath, "..", "shared-uploads"));
if (!Directory.Exists(sharedUploadsFolder))
{
    Directory.CreateDirectory(sharedUploadsFolder);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(sharedUploadsFolder),
    RequestPath = "/uploads"
});

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorPages();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
