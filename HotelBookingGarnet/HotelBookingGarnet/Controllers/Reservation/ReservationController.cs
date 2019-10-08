using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingGarnet.Controllers.Reservation
{
    
    
    [Authorize(Roles = "Guest, Admin")]
    public class ReservationController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IReservationService reservationService;

        public ReservationController(UserManager<User> userManager, IReservationService reservationService)
        {
            this.userManager = userManager;
            this.reservationService = reservationService;
        }

        [HttpGet("/newReservation/{roomId}")]
        public IActionResult AddReservation()
        {
            return View(new ReservationViewModel());
        }

        [HttpPost("/newReservation")]
        public async Task<IActionResult> AddReservation(ReservationViewModel newReservation)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await userManager.GetUserAsync(HttpContext.User);
                await reservationService.AddReservationAsync(newReservation, currentUser.Id);
                return RedirectToAction(nameof(ReservationController.ConfirmationPage), "Reservation");
            }

            return View(newReservation);
        }
    }
}