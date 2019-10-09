using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingGarnet.Controllers
{
    public class ReservationController : Controller
    {

        private readonly IReservationService reservationService;
        private readonly UserManager<User> userManager;
        private readonly IHotelService hotelService;
        private readonly IRoomService roomService;

        public ReservationController(IReservationService reservationService, UserManager<User> userManager, IHotelService hotelService, IRoomService roomService)
        {
            this.reservationService = reservationService;
            this.userManager = userManager;
            this.hotelService = hotelService;
            this.roomService = roomService;
        }

        [Authorize(Roles = "Guest")]
        [HttpGet("/myreservation")]
        public async Task<IActionResult> MyReservation()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            var reservations = await reservationService.FindReservationByIdAsync(currentUser.Id);
            var hotel =  hotelService.GetHotels();
            return View(new IndexViewModel { Reservation = reservations, HotelList = hotel });

        }
    }
}
