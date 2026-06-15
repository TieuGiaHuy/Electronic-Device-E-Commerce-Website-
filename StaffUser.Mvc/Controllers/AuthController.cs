using BLL.DTOs.Auth;
using BLL.DTOs.Customer;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using StaffUser.Mvc.ViewModels;

namespace StaffUser.Mvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly ICustomerService _customerService;

        public AuthController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }

            var customer = await _customerService.ValidateLoginAsync(loginDto.Email, loginDto.Password);
            if (customer == null)
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View(loginDto);
            }

            // Create claims and sign in with cookie authentication
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, customer.CustomerId.ToString()),
                new Claim(ClaimTypes.Name, customer.FullName ?? customer.Email),
                new Claim(ClaimTypes.Email, customer.Email),
                new Claim(ClaimTypes.Role, customer.Role.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = loginDto.RememberMe,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                ModelState.AddModelError("", "Please provide your email address.");
                return View();
            }

            // Verify the email exists before allowing reset
            var customer = await _customerService.GetByEmailAsync(email);
            if (customer == null)
            {
                ModelState.AddModelError("", "Email address not found.");
                return View();
            }

            // For a simplified dev flow, redirect the user to a page
            // where they can set a new password. In production use a secure token+email.
            return RedirectToAction("SetNewPassword", new { email = email });
        }

        public IActionResult SetNewPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return RedirectToAction("ForgotPassword");
            var model = new SetNewPasswordViewModel { Email = email };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetNewPassword(ViewModels.SetNewPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("", "Passwords do not match.");
                return View(model);
            }

            var customer = await _customerService.GetByEmailAsync(model.Email);
            if (customer == null)
            {
                // Don't reveal whether the email exists. Redirect to login with no details.
                return RedirectToAction("Login");
            }

            await _customerService.SetPasswordAsync(customer.CustomerId, model.Password);
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CustomerCreateDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return View(registerDto);
            }

            var existingCustomer = await _customerService.GetByEmailAsync(registerDto.Email);
            if (existingCustomer != null)
            {
                ModelState.AddModelError("", "Email already registered");
                return View(registerDto);
            }

            var customer = await _customerService.CreateAsync(registerDto);
            return RedirectToAction("Login", "Auth");
        }
    }
}