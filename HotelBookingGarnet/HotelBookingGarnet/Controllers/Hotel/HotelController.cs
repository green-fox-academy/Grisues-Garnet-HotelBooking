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
using Microsoft.WindowsAzure.Storage;

namespace HotelBookingGarnet.Controllers.Hotel
{
    public class HotelController : Controller
    {
        private readonly IHotelService hotelService;
        private readonly UserManager<User> userManager;
        private readonly IPropertyTypeService propertyTypeService;
        private readonly IImageService imageService;
        private readonly IRoomService roomService;
        private readonly IBedService bedService;
        private readonly IRoomBedService roomBedService;

        public HotelController(IHotelService hotelService, UserManager<User> userManager, IPropertyTypeService propertyTypeService, IImageService imageService, 
            IRoomService roomService, IBedService bedService, IRoomBedService roomBedService)
        {
            this.hotelService = hotelService;
            this.userManager = userManager;
            this.propertyTypeService = propertyTypeService;
            this.imageService = imageService;
            this.roomService = roomService;
            this.bedService = bedService;
            this.roomBedService = roomBedService;
        }

        [AllowAnonymous]
        [HttpGet("/info/{hotelId}")]
        public async Task<IActionResult> HotelInfo(long hotelId)
        { 
            var currentUser = await userManager.GetUserAsync(HttpContext.User); 
            var hotel = await hotelService.FindHotelByIdAsync(hotelId); 
            var blobsUri = await imageService.ListAsync(hotelId);
            var property = await propertyTypeService.FindPropertyByHotelIdAsync(hotelId);
            var roomBeds = roomBedService.GetRoomBeds();
            ViewData["propertyType"] = property;
            return View(new IndexViewModel { User = currentUser, Hotel = hotel, RoomBeds = roomBeds,blobs = blobsUri});

        }

        [Authorize(Roles = "Hotel Manager, Admin")]
        [HttpGet("/edit/{hotelId}")]
        public async Task<IActionResult> EditHotel(long hotelId)
        {
            var hotel = await hotelService.FindHotelByIdAsync(hotelId);
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            var property = await propertyTypeService.FindPropertyByHotelIdAsync(hotelId);
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
            hotelViewModel.PropertyType = property;
            
            hotelViewModel.StarRating = hotel.StarRating;
            return View(hotelViewModel);
        }
        
        [Authorize(Roles = "Hotel Manager, Admin")]
        [HttpPost("/edit/{hotelId}")]
        public async Task<IActionResult> EditHotel(HotelViewModel editHotel, long hotelId)
        {
            if (ModelState.IsValid)
            {
                var errors = imageService.Validate(editHotel.Files, editHotel);
                if (errors.Count !=0)
                {
                    return View(editHotel);
                }
                await hotelService.EditHotelAsync(hotelId, editHotel);
                await imageService.UploadAsync(editHotel.Files, hotelId);
                return RedirectToAction(nameof(HotelController.HotelInfo), "Hotel", new { hotelId });
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
                var errors = imageService.Validate(newHotel.Files,newHotel);
                if (errors.Count != 0)
                {
                    return View(newHotel);
                }
                var currentUser = await userManager.GetUserAsync(HttpContext.User); 
                var hotelId = await hotelService.AddHotelAsync(newHotel, currentUser.Id);
                await imageService.UploadAsync(newHotel.Files, hotelId);
                return RedirectToAction(nameof(HotelController.HotelInfo), "Hotel", new { hotelId });
            }
            return View(newHotel);
        }
        
        [Authorize(Roles = "Hotel Manager, Admin")]
        [HttpGet("/addroom/{hotelId}")]
        public IActionResult AddRoom(long hotelId)
        {
            ViewData["hotelId"] = hotelId;
            return View(new RoomViewModel());
        }
        
        [Authorize(Roles = "Hotel Manager, Admin")]
        [HttpPost("/addroom/{hotelId}")]
        public async Task<IActionResult> AddRoom(RoomViewModel newRoom, long hotelId)
        {
            if (ModelState.IsValid)
            {
                await roomService.AddRoomAsync(newRoom, hotelId);
                return RedirectToAction(nameof(HotelController.HotelInfo),"Hotel", new {hotelId});
            }
            return View(newRoom);
        }
        
        [Authorize(Roles = "Hotel Manager, Admin")]
        [HttpGet("/addbed/{hotelId}/{roomId}")]
        public IActionResult AddBed(long hotelId, long roomId)
        {
            ViewData["roomId"] = roomId;
            ViewData["hotelId"] = hotelId;
            return View(new BedViewModel());
        }

        [Authorize(Roles = "Hotel Manager, Admin")]
        [HttpPost("/addbed/{hotelId}/{roomId}")]
        public async Task<IActionResult> AddBed(BedViewModel newBed, long hotelId, long roomId)
        {
            if (ModelState.IsValid)
            {
                await bedService.AddBedAsync(newBed, roomId);
                return RedirectToAction(nameof(HotelController.HotelInfo),"Hotel", new {hotelId});
            }
            
            return View(newBed);
        }
    }
}