using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingGarnet.Controllers.Reservation
{
    [Authorize(Roles = "Guest, Admin, Hotel Manager")]
    public class ReservationController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IReservationService reservationService;
        private readonly IGuestService guestService;

        public ReservationController(UserManager<User> userManager, IReservationService reservationService,
            IGuestService guestService)
        {
            this.userManager = userManager;
            this.reservationService = reservationService;
            this.guestService = guestService;
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
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            var reservationId =
                await reservationService.AddReservationAsync(newReservation, currentUser.Id, roomId, hotelId);
            return RedirectToAction(nameof(ConfirmationPage), "Reservation", new {reservationId});
        }

        [HttpGet("/confirmation/{reservationId}")]
        public async Task<IActionResult> ConfirmationPage(long reservationId)
        {
            var reservation = await reservationService.FindReservationByIdAsync(reservationId);
            return View(reservation);
        }
    }
}