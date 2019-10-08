using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingGarnet.Controllers
{
    public class ReservationController : Controller
    {

        private readonly IReservationService reservation;
        private readonly UserManager<User> userManager;

        public ReservationController(IReservationService reservation, UserManager<User> userManager)
        {
            this.reservation = reservation;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Guest")]
        [HttpGet("/myreservation")]
        public async Task<IActionResult> MyReservation()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            var reservations = await reservation.FindReservationByIdAsync(currentUser.Id);
            return View(reservation);

        }
    }
}
