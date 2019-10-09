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
        private readonly IHotelService hotelService;

        public ReservationController(UserManager<User> userManager, IReservationService reservationService, IHotelService hotelService)
        {
            this.userManager = userManager;
            this.reservationService = reservationService;
            this.hotelService = hotelService;
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
//                var currentUser = await userManager.GetUserAsync(HttpContext.User);
//                await reservationService.AddReservationAsync(newReservation, currentUser.Id);
//                return RedirectToAction(nameof(ReservationController.ConfirmationPage), "Reservation");
            }
            return View(newReservation);
        }
        [Authorize(Roles = "Hotel Manager, Admin")]
        [HttpGet("/hotelReservation/{hotelId}")]
        public async Task<IActionResult> HotelReservation(long hotelId)
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            var hotelReservations = await reservationService.FindReservationByHotelIdAsync(hotelId);
            var hotel = await hotelService.FindHotelByIdAsync(hotelId);
            
            return View(new IndexViewModel{Reservations = hotelReservations, Hotel = hotel, User = currentUser});
        }
    }
}