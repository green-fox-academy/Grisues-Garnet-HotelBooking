using System.Threading.Tasks;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingGarnet.Controllers.Hotel
{
    public class HotelController : Controller
    {
        private readonly IHotelService hotelService;

        public HotelController(IHotelService hotelService)
        {
            this.hotelService = hotelService;
        }

        [HttpGet("/addhotel")]
        public IActionResult AddHotel(long userId)
        {
            return View(userId);
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