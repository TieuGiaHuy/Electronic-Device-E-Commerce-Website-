using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UserStaff.Razor.Pages.Auth
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly IStaffService _staffService;

        public ForgotPasswordModel(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                ModelState.AddModelError(string.Empty, "Please provide your email address.");
                return Page();
            }

            // Verify the email exists before allowing password reset
            var staff = _staffService.GetByEmailAsync(Email).GetAwaiter().GetResult();
            if (staff == null)
            {
                ModelState.AddModelError(string.Empty, "Email address not found.");
                return Page();
            }

            return RedirectToPage("SetNewPassword", new { email = Email });
        }
    }
}
