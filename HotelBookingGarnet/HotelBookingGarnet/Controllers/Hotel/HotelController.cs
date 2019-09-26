using System.Threading.Tasks;
using HotelBookingGarnet.Models;
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
        [HttpGet("/addhotel")]
        public IActionResult AddHotel()
        {
            return View();
        }

        [Authorize(Roles = "Hotel Manager")]
        [HttpPost("/addhotel")]
        public async Task<IActionResult> AddHotel(HotelViewModel newHotel)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await userManager.GetUserAsync(HttpContext.User);
                await hotelService.AddHotelAsync(newHotel, currentUser.UserId);
                RedirectToAction("Home", "Home");
            }
            return View();
        }
        
    }
}