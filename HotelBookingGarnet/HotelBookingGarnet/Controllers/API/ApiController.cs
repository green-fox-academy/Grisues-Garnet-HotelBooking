using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentEmail.Mailgun.HttpHelpers;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingGarnet.Controllers.API
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IHotelService hotelService;
        

        public ApiController(IHotelService hotelService)
        {
            this.hotelService = hotelService;
        }

        [HttpGet("/api/myhotels/{userId?}")]
        public async Task<ActionResult<List<Models.Hotel>>> ListHotelsByUserId(string userId)
        {
            var hotel = await hotelService.ListMyHotelsAsync(userId);
            if (userId == null)
            {
                return BadRequest("No user with this user ID");
            }
            if (hotel.Count == 0)
            {
                return BadRequest("No hotel found with this user ID");
            }

            return hotel;
        }

        [HttpPut("/api/addhotel/{userId}")]
        public async Task<ActionResult<Models.Hotel>> AddHotel([FromBody] HotelViewModel apiHotelViewModel, [FromRoute] string userId)
        {
           await hotelService.AddHotelAsync(apiHotelViewModel, userId);
           return Ok("Good job Kriszti!");
        }
    }
}