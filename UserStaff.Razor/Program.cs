using BLL;
using BLL.DTOs.Staff;
using BLL.Services.Interfaces;
using BLL.Settings;
using DAL.Enums;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");  

// Add services to the container.
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("StaffOnly", p => p.RequireRole(
        ((int)RoleEnum.Staff).ToString(),
        ((int)RoleEnum.Admin).ToString()));
});

builder.Services.AddRazorPages(options => { 
    options.Conventions.AuthorizeFolder("/", "StaffOnly");
    options.Conventions.AllowAnonymousToFolder("/Auth");
});

builder.Services.AddBusinessLogicLayer(connectionString!);
builder.Services.Configure<VnpaySettings>(builder.Configuration.GetSection("VNPAY"));

// Add authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
    });
builder.Services.AddAuthorization();
builder.Services.AddSession();

var app = builder.Build();

// Seed test staff account
using (var scope = app.Services.CreateScope())
{
    var staffService = scope.ServiceProvider.GetRequiredService<IStaffService>();
    var existingStaff = staffService.GetByEmailAsync("staff@local").GetAwaiter().GetResult();
    if (existingStaff == null)
    {
        var createDto = new StaffCreateDto
        {
            FullName = "Test Staff",
            Email = "staff@local",
            Password = "P@ssw0rd",
            Role = (int)RoleEnum.Staff, // Staff role
            IsActive = true
        };
        staffService.CreateAsync(createDto).GetAwaiter().GetResult();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();

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

app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
