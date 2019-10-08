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

        private readonly IReservationService reservationService;
        private readonly UserManager<User> userManager;

        public ReservationController(IReservationService reservationService, UserManager<User> userManager)
        {
            this.reservationService = reservationService;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Guest")]
        [HttpGet("/myreservation")]
        public async Task<IActionResult> MyReservation()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            var reservations = await reservationService.FindReservationByIdAsync(currentUser.Id);
            return View(reservations);

        }
    }
}
