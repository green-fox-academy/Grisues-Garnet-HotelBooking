using System.Threading.Tasks;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.Utils;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Mvc;



namespace HotelBookingGarnet.Controllers.Home
{
    public class HomeController : Controller
    {
        private readonly IHotelService hotelService;
        private readonly IUserService userService;

        public HomeController(IHotelService hotelService, IUserService userService)
        {
            this.hotelService = hotelService;
            this.userService = userService;
        }

        [HttpGet("/")]
        public async Task<IActionResult> Index(QueryParam queryParam)
        {
            var hotels = await hotelService.FilterHotelsAsync(queryParam);
            return View(new IndexViewModel
            {
                PagingList =  hotels,
                QueryParam = queryParam,
                ActionName = nameof(Index)
            });
        }

        [HttpPost("/logout")]
        public async Task<IActionResult> Logout()
        {
            await userService.Logout();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}