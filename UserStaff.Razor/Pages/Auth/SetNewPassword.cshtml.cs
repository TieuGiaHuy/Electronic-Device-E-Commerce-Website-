using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UserStaff.Razor.Pages.Auth
{
    public class SetNewPasswordModel : PageModel
    {
        private readonly IStaffService _staffService;

        public SetNewPasswordModel(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        [BindProperty]
        public string ConfirmPassword { get; set; } = string.Empty;

        public void OnGet(string? email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                Email = email;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Email)) return RedirectToPage("ForgotPassword");

            if (string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                ModelState.AddModelError(string.Empty, "Please fill in all password fields.");
                return Page();
            }

            if (Password != ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match.");
                return Page();
            }

            // Find staff and set new password (BLL will hash internally)
            var staff = await _staffService.GetByEmailAsync(Email);
            if (staff == null)
            {
                return RedirectToPage("/Auth/Login");
            }

            await _staffService.SetPasswordAsync(staff.StaffId, Password);
            return RedirectToPage("/Auth/Login");
        }
    }
}
