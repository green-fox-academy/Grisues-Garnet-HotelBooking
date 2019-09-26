using System.Threading.Tasks;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingGarnet.Controllers.Hotel
{
    public class HotelController : Controller
    {
        private readonly IHotelService hotelService;
        private readonly UserManager<User> userManager;

        public HotelController(IHotelService hotelService, UserManager<User> userManager)
        {
            this.hotelService = hotelService;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Hotel Manager")]
        [HttpGet("/edit/{HotelId}")]
        public async Task<IActionResult> EditHotel(long HotelId, HotelViewModel hotelViewModel)
        {
            var hotel = await hotelService.findHotelByIdAsync(HotelId);
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            hotelViewModel.User = currentUser;
            ViewData["HotelId"] = hotel.HotelId;
            hotelViewModel.Address = hotel.Address;
            hotelViewModel.City = hotel.City;
            hotelViewModel.Country = hotel.Country;
            hotelViewModel.Description = hotel.Description;
            hotelViewModel.Price = hotel.Price;
            hotelViewModel.Region = hotel.Region;
            hotelViewModel.HotelName = hotel.HotelName;
            hotelViewModel.PropertyType = hotel.PropertyType;
            hotelViewModel.StarRating = hotel.StarRating;
            return View(hotelViewModel);
        }

        [HttpPost("/edit/{HotelId}")]
        public async Task<IActionResult> EditHotel(HotelViewModel editHotel, long HotelId)
        { 
            if(ModelState.IsValid) 
            {
                await hotelService.editHotelAsync(HotelId, editHotel);
                return RedirectToAction("Home","Home"); 
            } 
            return View(editHotel);
        }
    }
}