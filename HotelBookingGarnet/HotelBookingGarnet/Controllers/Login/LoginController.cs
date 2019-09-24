using System.Threading.Tasks;
using HotelBookingGarnet.Controllers.Home;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingGarnet.Controllers.Login
{
    [Authorize]
    public class LoginController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;

        public LoginController(SignInManager<User> signInManager, IUserService userService)
        {
            _userService = userService;
            _signInManager = signInManager;
        }

        [HttpGet("/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                // await _signInManager.SignInAsync(user, isPersistent: false); ??
                if (result.Succeeded)
                {
                    var author = await _userService.FindByEmailAsync(model.Email); //usernamet returnolni
                    return RedirectToAction(nameof(HomeController.Index), "Home", new {username});
                }
            }

            return View();
        }
    }
}