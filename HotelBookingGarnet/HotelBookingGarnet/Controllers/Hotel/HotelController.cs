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
        private readonly UserManager<User> userManager;
        private readonly IPropertyTypeService propertyTypeService;

        public HotelController(IHotelService hotelService, UserManager<User> userManager, IPropertyTypeService propertyTypeService)
        {
            this.hotelService = hotelService;
            this.userManager = userManager;
            this.propertyTypeService = propertyTypeService;
        }

        [HttpPost("/info/{HotelId}")]
        public async Task<IActionResult> HotelInfo(long HotelId)
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            var hotel = await hotelService.FindHotelByIdAsync(HotelId);asdasd
            var property = await propertyTypeService.FindByIdAsync(hotel.PropertyTypeId);
            return View(new IndexViewModel { user = currentUser, hotel = hotel, propertyType = property });
        }
    }
}