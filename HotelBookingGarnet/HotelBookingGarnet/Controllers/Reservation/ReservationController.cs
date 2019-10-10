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
        private readonly UserManager<User> userManager;
        private readonly IReservationService reservationService;
        private readonly IHotelService hotelService;

        public ReservationController(UserManager<User> userManager, IReservationService reservationService, IHotelService hotelService)
        {
            this.userManager = userManager;
            this.reservationService = reservationService;
            this.hotelService = hotelService;
        }

        [Authorize(Roles = "Guest")]
        [HttpGet("/myreservation")]
        public async Task<IActionResult> MyReservation()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            var reservations = await reservationService.FindReservationsByIdAsync(currentUser.Id);
            var hotel = hotelService.GetHotels();
            return View(new IndexViewModel { Reservations = reservations, HotelList = hotel });

        }
        [HttpPost("/cancelreservation/{reservationId}")]
        public async Task<IActionResult> CancelReservation(long reservationId)
        {
            await reservationService.DeleteReservationById(reservationId);
            return RedirectToAction(nameof(ReservationController.MyReservation), "Reservation" );
        }

        [Authorize(Roles = "Hotel Manager, Admin")]
        [HttpGet("/hotelReservation/{hotelId}")]
        public async Task<IActionResult> HotelReservation(long hotelId)
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            var hotelReservations = await reservationService.FindReservationsByHotelIdAsync(hotelId);
            var hotel = await hotelService.FindHotelByIdAsync(hotelId);
            
            return View(new IndexViewModel{Reservations = hotelReservations, Hotel = hotel, User = currentUser});
        }
    }
}