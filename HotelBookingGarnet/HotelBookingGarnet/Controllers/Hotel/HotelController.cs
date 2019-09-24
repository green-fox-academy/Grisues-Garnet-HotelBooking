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
        
        [HttpGet("/edit")]
        public async Task<IActionResult> EditHotel(long HotelId)
        {
            var hotel = await hotelService.findHotelByIdAsync(HotelId);
            return View();
        }

        [HttpPost("/edit")]
        public async Task<IActionResult> EditHotelInfo(HotelViewModel editHotel, long HotelId)
        {
            await hotelService.editHotelAsync(HotelId, editHotel);
            return Redirect("/");
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}