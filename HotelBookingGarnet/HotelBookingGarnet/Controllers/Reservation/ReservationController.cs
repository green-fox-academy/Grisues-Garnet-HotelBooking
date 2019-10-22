using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBookingGarnet.Controllers.Hotel;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingGarnet.Controllers
{
    [Authorize(Roles = "Guest, Admin, Hotel Manager")]
    public class ReservationController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IReservationService reservationService;
        private readonly IHotelService hotelService;
        private readonly IGuestService guestService;

        public ReservationController(UserManager<User> userManager, IReservationService reservationService,
            IHotelService hotelService, IGuestService guestService)
        {
            this.userManager = userManager;
            this.reservationService = reservationService;
            this.hotelService = hotelService;
            this.guestService = guestService;
        }

        [Authorize(Roles = "Guest, Hotel Manager")]
        [HttpGet("/myreservation")]
        public async Task<IActionResult> MyReservation()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            var reservations = await reservationService.FindReservationByReservationIdAsync(currentUser.Id);
            var hotel = hotelService.GetHotels();

            return View(new IndexViewModel {Reservations = reservations, HotelList = hotel, User = currentUser});
        }

        [HttpPost("/cancelreservation/{reservationId}")]
        public async Task<IActionResult> CancelReservation(long reservationId)
        {
            await reservationService.DeleteReservationByIdAsync(reservationId);
            return RedirectToAction(nameof(ReservationController.MyReservation), "Reservation");
        }

        [Authorize(Roles = "Hotel Manager, Admin")]
        [HttpGet("/hotelReservation/{hotelId}")]
        public async Task<IActionResult> HotelReservation(long hotelId)
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            var hotelReservations = await reservationService.FindReservationsByHotelIdAsync(hotelId);
            var hotel = await hotelService.FindHotelByIdAsync(hotelId);

            return View(new IndexViewModel {Reservations = hotelReservations, Hotel = hotel, User = currentUser});
        }

        [HttpGet("/newReservation/{roomId}/{hotelId}")]
        public IActionResult AddReservation(long roomId, long hotelId)
        {
            ViewData["roomId"] = roomId;
            ViewData["hotelId"] = hotelId;
            return View(new ReservationViewModel());
        }

        [HttpPost("/newReservation/{roomId}/{hotelId}")]
        public async Task<IActionResult> AddReservation(ReservationViewModel newReservation, long roomId, long hotelId)
        {
            if (ModelState.IsValid)
            {
                var errors = await reservationService.ReservationValidationAsync(newReservation, roomId);
                if (errors.Count == 0)
                {
                    var currentUser = await userManager.GetUserAsync(HttpContext.User);
                    var reservationId =
                        await reservationService.AddReservationAsync(newReservation, currentUser.Id, roomId, hotelId);
                    return RedirectToAction(nameof(ConfirmationPage), "Reservation", new {reservationId});
                }

                newReservation.ErrorMessages = errors;
                return View(newReservation);
            }

            return View(newReservation);
        }

        [HttpGet("/confirmation/{reservationId}")]
        public async Task<IActionResult> ConfirmationPage(long reservationId)
        {
            var reservation = await reservationService.FindReservationByReservationIdAsync(reservationId);
            return View(reservation);
        }

        [Authorize(Roles = "Guest")]
        [HttpPost("/cleanreservation")]
        public async Task<IActionResult> CleanReservation(string userId)
        {
            await reservationService.DeleteExpiredReservationByIdAsync(userId);

            return RedirectToAction(nameof(ReservationController.MyReservation), "Reservation");
        }
        
    }
}