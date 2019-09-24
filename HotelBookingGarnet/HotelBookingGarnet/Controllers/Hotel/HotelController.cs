using System.Threading.Tasks;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingGarnet.Controllers.Hotel
{
    public class HotelController : Controller
    {
        private readonly IHotelService hotelService;
        private readonly IUserService userService;

        public HotelController(IHotelService hotelService, IUserService userService)
        {
            this.hotelService = hotelService;
            this.userService = userService;
        }

        [HttpGet("/addhotel")]
        public async Task<IActionResult> AddHotel(long userId)
        {
            var user = await userService.FindByUserIdAsync(userId);
            return View(user);
        }

        [HttpPost("/addhotel")]
        public async Task<IActionResult> AddHotel(HotelViewModel newHotel, long userId)
        {
            if (ModelState.IsValid)
            {
                await hotelService.AddHotelAsync(newHotel, userId);
            }
            return View();
        }
        
    }
}