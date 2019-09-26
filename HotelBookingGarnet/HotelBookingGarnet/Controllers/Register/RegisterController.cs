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
        private readonly IUserService userService;

        public RegisterController(IUserService UserService)
        {
            userService = UserService;
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

            var result = await userService.RegisterAsync(model);

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
