using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Services;
using Microsoft.AspNetCore.Mvc;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal;
using Newtonsoft.Json.Serialization;

namespace HotelBookingGarnet.Controllers.API
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        
        private readonly IHotelService hotelService;
        private readonly IApiHotelService apiHotelService;
        private readonly IReservationService reservationService;
        private readonly IMapper mapper;

        public ApiController(IHotelService hotelService, IApiHotelService apiHotelService, IReservationService reservationService, IMapper mapper)
        {
            this.hotelService = hotelService;
            this.apiHotelService = apiHotelService;
            this.reservationService = reservationService;
            this.mapper = mapper;
        }

        [HttpGet("/api/hotels")]
        public async Task<ActionResult<ApiViewModel>> ListHotels([FromQuery] string city)
        {
            var hotelList = await hotelService.GetHotelsAsync();
            var model = new ApiViewModel
            {
                HotelList = hotelList,
                PageCount = 4,
                CurrentPage = 1
            };
            if (city == null)
                return model;
            
            model.HotelList = await apiHotelService.FindHotelByCityAsync(city);
            return model;
        }

        [HttpGet("/api/hotels/{hotelId}/rooms")]
        public async Task<ActionResult<List<Room>>> ListRoomsByHotelId(long hotelId)
        {
            var hotel = await hotelService.FindHotelByIdAsync(hotelId);
            if (hotel == null)
            {
                return BadRequest("No hotel found under this id!");
            }
            return hotel.Rooms;
        }

        [HttpPut("api/reservation")]
        public async Task<ActionResult> ReserveHotel([FromBody] ApiReservationViewModel model)
        {
            var modelForReservation = mapper.Map<ApiReservationViewModel, ReservationViewModel>(model);
            var roomId = model.RoomId;
            if (ModelState.IsValid)
            {
                var errors = reservationService.ReservationValidation(modelForReservation, roomId);
                if (errors.Count == 0)
                {
                    var apiUser = new User
                    {
                        Id = "5139a295-3981-4320-98c2-bb58b3e53032",
                        AccessFailedCount = 0
                    };
                    var reservationId =
                        await reservationService.AddReservationAsync(modelForReservation, apiUser.Id, roomId, model.HotelId);
                    return new OkObjectResult($"Successful reservation! Your reservation ID is {reservationId}!");
                }
                modelForReservation.ErrorMessages = errors;
                BadRequest(errors);
            }

            return Content("Invalid reservation!");
        }
    }
}