using Microsoft.AspNetCore.Mvc;

namespace HotelBookingGarnet.Controllers.Login
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
            await _signInManager.SignInAsync(user, false);
            new { username }
        private readonly SignInManager<User> _signInManager;
        }
    }
}