using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingGarnet.Controllers
{
    public class UserController : Controller
    {
        [HttpGet("/register")]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
    }
}