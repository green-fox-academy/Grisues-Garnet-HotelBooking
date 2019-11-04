using System;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using AutoMapper.Configuration.Internal;
using FluentEmail.Core;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace HotelBookingGarnet.Controllers
{
    public class AppUserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;
        private readonly IStringLocalizer<AppUserController> _localizer;

        public AppUserController(UserManager<User> userManager, IUserService userService,
            IStringLocalizer<AppUserController> localizer)
        {
            this.userManager = userManager;
            this.userService = userService;
            _localizer = localizer;
        }

        [Authorize]
        [HttpGet("/settings")]
        public async Task<IActionResult> Settings()
        {
            ViewData["message"] = "";
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            return View(new SettingsViewModel {User = currentUser});
        }

        [Authorize]
        [HttpPost("/settings")]
        public async Task<IActionResult> Settings(string culture, string returnUrl, SettingsViewModel settingsViewModel)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (settingsViewModel.OldPassword == null)
            {
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions {Expires = DateTimeOffset.UtcNow.AddDays(1)});
                return LocalRedirect(returnUrl);
            }

            if (ModelState.IsValid)
            {
                if (settingsViewModel.OldPassword == settingsViewModel.NewPassword)
                {
                    ViewData["error"] = _localizer["New password and old password can't be the same!"];
                    return View(new SettingsViewModel {User = user});
                }

                if (settingsViewModel.NewPassword != settingsViewModel.OldPassword)
                {
                    var result = await userManager.ChangePasswordAsync(user,
                        settingsViewModel.OldPassword, settingsViewModel.NewPassword);
                    if (result.Succeeded)
                    {
                        ViewData["message"] = "Password change was successful!";
                        return View(new SettingsViewModel {User = user});
                    }
                }
            }

            return View(new SettingsViewModel {User = user});
        }

        [HttpGet("/forgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View(new LoginViewModel());
        }

        [HttpPost("/forgotPassword")]
        public async Task<IActionResult> SendRecoveryEmail(LoginViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            ModelState.Remove("Password");

            if (!ModelState.IsValid)
            {
                return View("~/Views/AppUser/ForgotPassword.cshtml", model);
            }

            var errors = await userService.IsEmailPresent(model);
            if (errors.Count != 0)
            {
                model.ErrorMessages = errors;
                return View("~/Views/AppUser/ForgotPassword.cshtml", model);
            }

            var newRandomPassword = userService.GenerateNewPassword();
            await userService.ChangePasswordAsync(newRandomPassword, user);
            await userService.SendRecoveryPasswordAsync(model.Email, newRandomPassword);
            return View("~/Views/AppUser/ResetConfirmation.cshtml");
        }
    }
}