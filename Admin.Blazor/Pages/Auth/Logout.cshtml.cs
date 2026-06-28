using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Blazor.Pages.Auth
{
    [IgnoreAntiforgeryToken]
    public class Logout : PageModel
    {
        public IActionResult OnGet()
        {
            // Removes the auth cookie for the current user
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).GetAwaiter().GetResult();
            return LocalRedirect("/auth/login");
        }
    }
}