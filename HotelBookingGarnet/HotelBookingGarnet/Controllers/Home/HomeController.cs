using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelBookingGarnet.Controllers.Home
{
    public class HomeController : Controller
    {
        private readonly IHotelService hotelService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IUserService userService;

        public HomeController(IHotelService hotelService, UserManager<User> userManager, SignInManager<User> signInManager, IUserService userService)
        {
            this.hotelService = hotelService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userService = userService;
        }

        [HttpGet("/")]
        public IActionResult Index(int page = 1)
        {
            var hotels =  hotelService.GetHotels();
            var model = PagingList.Create(hotels, 5, page);
            return View(model);
        }
        
        [HttpPost("/logout")]
        public async Task<IActionResult> Logout()
        {
            await userService.Logout();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
