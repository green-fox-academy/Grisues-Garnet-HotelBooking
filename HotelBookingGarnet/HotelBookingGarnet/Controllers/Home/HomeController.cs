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
        private readonly IImageService imageService;

        public HomeController(IHotelService hotelService, IUserService userService, IImageService imageService)
        {
            this.hotelService = hotelService;
            this.userService = userService;
            this.imageService = imageService;
        }

        [HttpGet("/")]
        public async Task<IActionResult> Index(QueryParam queryParam)
        {
            var hotelList = hotelService.GetHotels();
            var hotels = await hotelService.FilterHotelsAsync(queryParam);
            return View(new IndexViewModel
            {
                HotelList = hotelList,
                PagingList = hotels,
                QueryParam = queryParam,
                ActionName = nameof(Index),
            });
        }

        [HttpPost("/logout")]
        public async Task<IActionResult> Logout()
        {
            await userService.LogoutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}