using System.Threading.Tasks;
using HotelBookingGarnet.Controllers.Home;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingGarnet.Controllers.Login
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IUserService userService;

        public LoginController(IUserService UserService)
        {
            userService = UserService;
        }

        [HttpGet("/login")]
        public IActionResult Login()
        {
            return View();
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
            }
            return View(model);
        }
    }
}
