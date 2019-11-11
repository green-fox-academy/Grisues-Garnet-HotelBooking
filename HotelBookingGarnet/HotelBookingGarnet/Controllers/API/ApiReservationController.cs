using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingGarnet.Controllers.API
{  
    [ApiController]
    public class ApiReservationController : ControllerBase
    {
        private readonly IReservationService reservationService;

        public ApiReservationController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        [HttpGet("/api/reservation/{userId}")]
        public async Task<ActionResult<List<Reservation>>> GetReservation([FromRoute]string userId)
        {
            var reservations = await reservationService.FindReservationByReservationIdAsync(userId);

            if (reservations.Count == 0)
            {
                return BadRequest("There is no reservation.");
            }
            
            return reservations;
        }

        [HttpDelete("/api/reservation/{reservationId}")]
        public async Task<ActionResult> DeleteReservation([FromRoute]long reservationId)
        {
            var reservation = await reservationService.FindReservationByReservationIdAsync(reservationId);
            if (reservation == null)
            {
                return BadRequest("Reservation not found.");
            }
            await reservationService.DeleteReservationByIdAsync(reservationId);
            return Ok();
        }
    }
}