using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Admin.Blazor.Pages.Auth
{
    [IgnoreAntiforgeryToken]
    public class LoginApi : PageModel
    {
        private readonly IStaffService _staffService;

        public LoginApi(IStaffService staffService)
        {
            _staffService = staffService;
        }

        public IActionResult OnPost(string email, string password)
        {
            // Validate credentials using BLL service which verifies the hashed password
            var staff = _staffService.ValidateLoginAsync(email, password).GetAwaiter().GetResult();
            if (staff == null)
            {
                return RedirectToPage("/auth/login");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, staff.StaffId.ToString()),
                new Claim(ClaimTypes.Name, staff.FullName),
                new Claim(ClaimTypes.Email, staff.Email),
                new Claim(ClaimTypes.Role, staff.Role.ToString())
            };

            // Create identity and sign in (issues the authentication cookie)
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).GetAwaiter().GetResult();

            return LocalRedirect("/");
        }
    }
}