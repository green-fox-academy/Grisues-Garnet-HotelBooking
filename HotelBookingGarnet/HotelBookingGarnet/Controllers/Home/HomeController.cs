using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
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

        public HomeController(IHotelService hotelService, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.hotelService = hotelService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet("/home")]
        public IActionResult Index(int page = 1)
        {
            var hotels = hotelService.GetHotels();
            var modell = PagingList.Create(hotels, 1, page);
            var model = new TestModel
            {
                Hotels = modell
            };
            return View(model);
        }
    }
}
