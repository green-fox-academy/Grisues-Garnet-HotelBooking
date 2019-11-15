﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HotelBookingGarnet.Utils;

namespace HotelBookingGarnet.Controllers.Hotel
{
    public class HotelController : Controller
    {
            private readonly IHotelService hotelService;
        private readonly UserManager<User> userManager;
        private readonly IImageService imageService;
        private readonly IRoomService roomService;
        private readonly IBedService bedService;
        private readonly IRoomBedService roomBedService;
        private readonly IHotelPropertyTypeService hotelPropertyTypeService;
        private readonly IDateTimeService dateTimeService;
        private readonly IMapper mapper;
        private readonly IReviewService reviewService;
        private readonly IReservationService reservationService;

        public HotelController(IHotelService hotelService, UserManager<User> userManager, IImageService imageService, 
            IRoomService roomService, IBedService bedService, IRoomBedService roomBedService, 
            IHotelPropertyTypeService hotelPropertyTypeService, IDateTimeService dateTimeService, IMapper mapper, IReviewService reviewService, IReservationService reservationService)
        {
            this.hotelService = hotelService;
            this.userManager = userManager;
            this.imageService = imageService;
            this.roomService = roomService;
            this.bedService = bedService;
            this.roomBedService = roomBedService;
            this.hotelPropertyTypeService = hotelPropertyTypeService;
            this.dateTimeService = dateTimeService;
            this.mapper = mapper;
            this.reviewService = reviewService;
            this.reservationService = reservationService;
        }

        [AllowAnonymous]
        [HttpGet("/info/{hotelId}")]
        public async Task<IActionResult> HotelInfo(long hotelId, QueryParam queryParam)
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            var blobsUri = await imageService.ListAsync(hotelId);
            var hotel = await hotelService.FindHotelByIdAsync(hotelId);
            var property = await hotelPropertyTypeService.FindPropertyByHotelIdAsync(hotelId);
            var roomBeds = roomBedService.GetRoomBeds();
            ViewData["propertyType"] = property.PropertyType.Type;
            ViewData["averageRating"] = hotelService.AverageRating(hotel.Reviews);
            var isReviewed = reviewService.Reviewed(hotel.Reviews, currentUser);
            var reviewsPaging = hotelService.ReviewsList(hotel.Reviews, queryParam);
            return View(new IndexViewModel
                {User = currentUser, Hotel = hotel, RoomBeds = roomBeds, FolderList = blobsUri, IsReviewed = isReviewed, ReviewsPagingList = reviewsPaging, QueryParam = queryParam, ActionName = nameof(HotelInfo)});
        }

        [Authorize(Roles = "Hotel Manager, Admin")]
        [HttpGet("/edit/{hotelId}")]
        public async Task<IActionResult> EditHotel(long hotelId)
        {
            var hotel = await hotelService.FindHotelByIdAsync(hotelId);
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            var property = await hotelPropertyTypeService.FindPropertyByHotelIdAsync(hotelId);
            var hotelViewModel = mapper.Map<Models.Hotel, HotelViewModel>(hotel);
            hotelViewModel.User = currentUser;
            ViewData["hotelId"] = hotel.HotelId;
            hotelViewModel.PropertyType = property.PropertyType.Type;
            hotelViewModel.StarRating = hotel.StarRating;
            ViewBag.TimeZones = dateTimeService.FindTimeZones();
            return View(hotelViewModel);
        }

        [Authorize(Roles = "Hotel Manager, Admin")]
        [HttpPost("/edit/{hotelId}")]
        public async Task<IActionResult> EditHotel(HotelViewModel editHotel, long hotelId)
        {
            if (ModelState.IsValid)
            {
                await hotelService.EditHotelAsync(hotelId, editHotel);
                if (editHotel.Files != null)
                {
                    var errors = imageService.Validate(editHotel.Files, editHotel);
                    if (errors.Count != 0)
                    {
                        ViewBag.TimeZones = dateTimeService.FindTimeZones();
                        return View(editHotel);
                    }

                    await imageService.UploadAsync(editHotel.Files, hotelId);
                    await hotelService.SetIndexImageAsync(hotelId);
                }

                await hotelPropertyTypeService.EditPropertyTypeAsync(hotelId, editHotel.PropertyType);
                return RedirectToAction(nameof(HotelController.HotelInfo), "Hotel", new {hotelId});
            }
            ViewBag.TimeZones = dateTimeService.FindTimeZones();
            return View(editHotel);
        }

        [Authorize(Roles = "Hotel Manager, Admin")]
        [HttpGet("/addhotel")]
        public IActionResult AddHotel()
        {
            ViewBag.TimeZones = dateTimeService.FindTimeZones();
            return View(new HotelViewModel());
        }

        [Authorize(Roles = "Hotel Manager, Admin")]
        [HttpPost("/addhotel")]
        public async Task<IActionResult> AddHotel(HotelViewModel newHotel)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await userManager.GetUserAsync(HttpContext.User);
                var hotelId = await hotelService.AddHotelAsync(newHotel, currentUser.Id);
                if (newHotel.Files != null)
                {
                    var errors = imageService.Validate(newHotel.Files, newHotel);
                    if (errors.Count != 0)
                    {
                        ViewBag.TimeZones = dateTimeService.FindTimeZones();
                        return View(newHotel);
                    }

                    await imageService.UploadAsync(newHotel.Files, hotelId);
                    await hotelService.SetIndexImageAsync(hotelId);
                }

                return RedirectToAction(nameof(HotelController.HotelInfo), "Hotel", new {hotelId});
            }
            ViewBag.TimeZones = dateTimeService.FindTimeZones();
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
                return RedirectToAction(nameof(HotelController.HotelInfo), "Hotel", new {hotelId});
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
                return RedirectToAction(nameof(HotelController.HotelInfo), "Hotel", new {hotelId});
            }
            return View(newBed);
        }
       
        [Authorize(Roles = "Hotel Manager, Admin")]
        [HttpGet("/myhotels")]
        public async Task<IActionResult> MyHotels()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            var myHotels = await hotelService.ListMyHotelsAsync(currentUser.Id);
            return View(myHotels);
        }
        
        [AllowAnonymous]
        [HttpPost("/review/{hotelId}")]
        public async Task<IActionResult> HotelReview(long hotelId, IndexViewModel newReview, QueryParam queryParam)
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                await reviewService.AddReviewAsync(hotelId, newReview, currentUser.Id);
                return RedirectToAction(nameof(HotelController.HotelInfo), "Hotel", new {hotelId});
            }
            var blobsUri = await imageService.ListAsync(hotelId);
            var hotel = await hotelService.FindHotelByIdAsync(hotelId);
            var property = await hotelPropertyTypeService.FindPropertyByHotelIdAsync(hotelId);
            var roomBeds = roomBedService.GetRoomBeds();
            ViewData["propertyType"] = property.PropertyType.Type;
            ViewData["averageRating"] = hotelService.AverageRating(hotel.Reviews);
            var reviewsPaging = hotelService.ReviewsList(hotel.Reviews, queryParam);
            newReview.User = currentUser;
            newReview.Hotel = hotel;
            newReview.RoomBeds = roomBeds;
            newReview.FolderList = blobsUri;
            newReview.ReviewsPagingList = reviewsPaging;
            return View("HotelInfo", newReview);
        }
        
        [AllowAnonymous]
        [HttpPost("/info/{hotelId}")]
        public async Task<IActionResult> HotelInfo(long hotelId, DateTime start, DateTime end, QueryParam queryParam)
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            
            var blobsUri = await imageService.ListAsync(hotelId);
            var hotel = await hotelService.FindHotelByIdAsync(hotelId);
            var property = await hotelPropertyTypeService.FindPropertyByHotelIdAsync(hotelId);
            var roomBeds = roomBedService.GetRoomBeds();
            ViewData["propertyType"] = property.PropertyType.Type;
            ViewData["averageRating"] = hotelService.AverageRating(hotel.Reviews);
            var isReviewed = reviewService.Reviewed(hotel.Reviews, currentUser);
            var rooms = await reservationService.FindAvailableRoomByHotelIdAndDateAsync(hotelId, start, end);
            var reviewsPaging = hotelService.ReviewsList(hotel.Reviews, queryParam);
            return View(new IndexViewModel 
                {User = currentUser, Hotel = hotel, RoomBeds = roomBeds, FolderList = blobsUri, IsReviewed = isReviewed, ReviewsPagingList = reviewsPaging, QueryParam = queryParam, Rooms = rooms, ActionName = nameof(HotelInfo)});
        }
    }
}