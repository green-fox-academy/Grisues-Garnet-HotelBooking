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
        public IUserService _userService { get; set; }

        public LoginController(SignInManager<User> signInManager, IUserService userService)
        {
            _signInManager = signInManager;
            _userService = userService;
        }

        [HttpGet("/login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("/login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    var user = await _userService.FindByEmailAsync(model.Email);
                    var userId = user.Id;
                    return RedirectToAction(nameof(HomeController.Index), "Home", new {userId});
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }

            return View(model);
        }
    }
}