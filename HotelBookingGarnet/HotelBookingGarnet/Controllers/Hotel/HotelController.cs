using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBookingGarnet.Services;
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

        [HttpPost("/info/{HotelId}")]
        public async Task<IActionResult>HotelInfo(long HotelId)
        {
           
           var hotel = await hotelService.FindHotelByIdAsync(HotelId);
            return View(hotel);
        }
    }
}