using System.Threading.Tasks;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingGarnet.Controllers.Register
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        public IUserService _userService { get; set; }

        public RegisterController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("/register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("/register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _userService.RegisterAsync(model);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
    }
}