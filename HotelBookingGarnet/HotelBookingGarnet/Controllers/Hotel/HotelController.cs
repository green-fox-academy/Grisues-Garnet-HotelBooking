using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HotelBookingGarnet.Controllers.Home;

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

        [Authorize]
        [HttpPost("/info/{hotelId}")]
        public async Task<IActionResult> HotelInfo(long hotelId)
        { 
            var currentUser = await userManager.GetUserAsync(HttpContext.User); 
            var hotel = await hotelService.FindHotelByIdAsync(hotelId); 
            var property = await propertyTypeService.FindByIdAsync(hotel.PropertyType.PropertyTypeId);
            ViewData["propertyType"] = property;
            return View(new IndexViewModel { User = currentUser, Hotel = hotel });
        }

        [Authorize(Roles = "Hotel Manager, Admin")]
        [HttpGet("/edit/{hotelId}")]
        public async Task<IActionResult> EditHotel(long hotelId)
        {
            var hotel = await hotelService.FindHotelByIdAsync(hotelId);
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            var hotelViewModel = new HotelViewModel();
            hotelViewModel.User = currentUser;
            ViewData["hotelId"] = hotel.HotelId;
            hotelViewModel.Address = hotel.Address;
            hotelViewModel.City = hotel.City;
            hotelViewModel.Country = hotel.Country;
            hotelViewModel.Description = hotel.Description;
            hotelViewModel.Price = hotel.Price;
            hotelViewModel.Region = hotel.Region;
            hotelViewModel.HotelName = hotel.HotelName;
            hotelViewModel.PropertyType = hotel.PropertyType.Type;
            hotelViewModel.StarRating = hotel.StarRating;
            return View(hotelViewModel);
        }
        
        [Authorize(Roles = "Hotel Manager, Admin")]
        [HttpPost("/edit/{hotelId}")]
        public async Task<IActionResult> EditHotel(HotelViewModel editHotel, long hotelId)
        {
            if (ModelState.IsValid)
            { 
                await hotelService.EditHotelAsync(hotelId, editHotel);
                return RedirectToAction(nameof(HomeController.Index),"Home");
            }
            return View(editHotel);
        }

        [Authorize(Roles = "Hotel Manager, Admin")]
        [HttpGet("/addhotel")]
        public IActionResult AddHotel()
        {
            return View(new HotelViewModel());
        }

        [Authorize(Roles = "Hotel Manager, Admin")]
        [HttpPost("/addhotel")]
        public async Task<IActionResult> AddHotel(HotelViewModel newHotel)
        {
            if (ModelState.IsValid)
            { 
                var currentUser = await userManager.GetUserAsync(HttpContext.User); 
                await hotelService.AddHotelAsync(newHotel, currentUser.Id); 
                return RedirectToAction(nameof(HomeController.Index),"Home");
            }
            return View(newHotel);
        }
    }
}