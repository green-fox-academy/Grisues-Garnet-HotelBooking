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

        public ReservationController(IReservationService reservationService, UserManager<User> userManager, IHotelService hotelService)
        {
            this.reservationService = reservationService;
            this.userManager = userManager;
            this.hotelService = hotelService;
        }

        [Authorize(Roles = "Guest")]
        [HttpGet("/myreservation")]
        public async Task<IActionResult> MyReservation()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            var reservations = await reservationService.FindReservationByIdAsync(currentUser.Id);
            var hotel = hotelService.GetHotels();
            return View(new IndexViewModel { Reservations = reservations, HotelList = hotel });

        }
        [HttpPost("/cancelreservation/{reservationId}")]
        public IActionResult CancelReservation(long ReservationId)
        {
            reservationService.DeleteReservationById(ReservationId);
            return RedirectToAction(nameof(ReservationController.MyReservation), "Reservation" );
        }
    }
}
