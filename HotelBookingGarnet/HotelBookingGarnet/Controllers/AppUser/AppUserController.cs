using System;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace HotelBookingGarnet.Controllers
{
    public class AppUserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;
        
        public AppUserController(UserManager<User> userManager, IUserService userService)
        {
            this.userManager = userManager;
            this.userService = userService;
        }

        [Authorize]
        [HttpGet("/settings")]
        public async Task<IActionResult> Settings()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            return View(new SettingsViewModel {User = currentUser});
        }

        [Authorize]
        [HttpPost("/settings")]
        public async Task<IActionResult> Settings(string culture, string returnUrl, SettingsViewModel settingsViewModel)
        {
            if (settingsViewModel.OldPassword == null)
            {
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions {Expires = DateTimeOffset.UtcNow.AddDays(1)});
                return LocalRedirect(returnUrl);
            } 
            if (settingsViewModel.OldPassword != null && settingsViewModel.NewPassword != null
                                                      && settingsViewModel.ConfirmPassword != null)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                var result = await userManager.ChangePasswordAsync(user,
                    settingsViewModel.OldPassword, settingsViewModel.NewPassword);
                if (result.Succeeded)
                {
                    ViewData["message"] = "Password change was successful!";
                    return View();
                }
            }
            return View();
        }
    }
}