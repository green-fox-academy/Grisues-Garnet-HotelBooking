using System.Security.Claims;
using System.Threading.Tasks;
using HotelBookingGarnet.Controllers.Home;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingGarnet.Controllers.Login
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public LoginController(IUserService userService, UserManager<User> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        [HttpGet("/login")]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            await userService.Logout();
            return View(new LoginViewModel());
        }

        [HttpPost("/login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var errors = await userService.LoginAsync(model);
                if (errors.Count == 0)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }

                model.ErrorMessages = errors;
                return View(model);
            }

            return View(model);
        }

        [HttpGet("/Google-login")]
        public IActionResult GoogleLogin()
        {
            var redirectUrl = "Google-response";
            var properties = userService.ConfigureExternalAutheticationProp("Google",redirectUrl);

            return new ChallengeResult("Google", properties);
        }
        
        [HttpGet("/Facebook-login")]
        public IActionResult FacebookLogin()
        {
            var redirectUrl = "Google-response";
            var properties = userService.ConfigureExternalAutheticationProp("Facebook",redirectUrl);

            return new ChallengeResult("Facebook", properties);
        }

        [HttpGet("/Google-response")]
        public async Task<IActionResult> GoogleResponse()
        {

            var info = await userService.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }
            
            var result = await userService.ExternalLoginSingnInAsync(info.LoginProvider, info.ProviderKey, false);
            
            if (!result.Succeeded)
            {
                await userService.CreateAndLoginGoogleUser(info);
            }
            return RedirectToAction(nameof(HomeController.Index),"Home");
        }
    }
}