using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingGarnet.Controllers.Home
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> userManager;

        public HomeController(UserManager<User> UserManager)
        {
            userManager = UserManager;
        }

        [HttpGet("/home")]
        public async Task<IActionResult> Index()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            return View(new IndexViewModel {User = currentUser});
        }
    }
}