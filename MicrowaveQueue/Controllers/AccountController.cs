using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MicrowaveQueue.Domain.Entities;
using MicrowaveQueue.Models;

namespace MicrowaveQueue.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IStringLocalizer<AccountController> _localizer;

        public AccountController(UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            ILogger<AccountController> logger,
            IStringLocalizer<AccountController> localizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _localizer = localizer;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewData["Title"] = _localizer["LoginTitle"];
            ViewData["BtnSubmitLogin"] = _localizer["BtnSubmitLogin"];
            ViewData["Registration"] = _localizer["Registration"];
            ViewData["UserName"] = _localizer["UserName"];
            ViewData["Password"] = _localizer["Password"];
            ViewData["RememberMe"] = _localizer["RememberMe"];

            _logger.LogInformation("Executing: [Get] Action='Login' on Controller='Account' in Area=''");

            var model = new LoginViewModel { ReturnUrl = returnUrl };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            _logger.LogInformation("Starting: [Post] Action='Login' on Controller='Account' in Area=''");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Error Model Validation on: [Post] Action='Login' on Controller='Account' in Area=''");
                _logger.LogInformation("Stop: [Post] Action='Login' on Controller='Account' in Area=''");

                return View(model);
            }

            var loginResult =
                await _signInManager.PasswordSignInAsync(model.UserName,
                    model.Password, model.RememberMe, false);

            if (!loginResult.Succeeded)
            {
                _logger.LogWarning("Login Error: [Post] Action='Login' on Controller='Account' in Area=''");

                ModelState.AddModelError("", "Ошибка авторизации.");
                return View(model);
            }

            _logger.LogInformation("Successful: [Post] Action='Login' on Controller='Account' in Area=''");

            if (Url.IsLocalUrl(model.ReturnUrl))
            {
                _logger.LogInformation($"Redirecting To: {model.ReturnUrl}");

                return Redirect(model.ReturnUrl);
            }

            _logger.LogInformation($"Redirecting To: Action='Index' on Controller='Home' in Area=''");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation("Executing: [Get] Action='Logout' on Controller='Account' in Area=''");

            await _signInManager.SignOutAsync();

            _logger.LogInformation($"Redirecting To: Action='Index' on Controller='Home' in Area=''");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewData["Title"] = _localizer["Registration"];
            ViewData["BtnSubmitRegister"] = _localizer["BtnSubmitRegister"];
            ViewData["UserName"] = _localizer["UserName"];
            ViewData["Password"] = _localizer["Password"];
            ViewData["ConfirmPassword"] = _localizer["ConfirmPassword"];

            _logger.LogInformation("Executing: [Get] Action='Register' on Controller='Account' in Area=''");

            return View(new RegisterUserViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            _logger.LogInformation("Starting: [Post] Action='Register' on Controller='Account' in Area=''");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Error Model Validation on: [Post] Action='Register' on Controller='Account' in Area=''");
                _logger.LogInformation("Stop: [Post] Action='Register' on Controller='Account' in Area=''");

                return View(model);
            }

            var user = new User { UserName = model.UserName };

            var createUserResult = await _userManager.CreateAsync(user, model.Password);

            if (!createUserResult.Succeeded)
            {
                _logger.LogWarning("Error Creating User");

                foreach (var error in createUserResult.Errors)
                {
                    _logger.LogWarning($"Error Creating User Details: {error.Description}");
                    _logger.LogInformation("Stop: [Post] Action='Register' on Controller='Account' in Area=''");

                    ModelState.AddModelError("", error.Description);

                    return View(model);
                }
            }

            _logger.LogInformation("Succsess Creating User");

            await _userManager.AddToRoleAsync(user, "Users");

            await _signInManager.SignInAsync(user, false);

            _logger.LogInformation("Successful: [Post] Action='Register' on Controller='Account' in Area=''");

            _logger.LogInformation($"Redirecting To: Action='Index' on Controller='Home' in Area=''");

            return RedirectToAction("Index", "Home");
        }
    }
}