using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelBookingGarnet.DTO;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace HotelBookingGarnet.Controllers.API
{
    
    [ApiController]
    [Route("api/")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService hotelService;
        private readonly IMapper mapper;

        public HotelController(IHotelService hotelService, IMapper mapper)
        {
            this.hotelService = hotelService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("hotels/{hotelId}/rooms")]
        public async Task<ActionResult<List<Room>>> Get(long hotelId)
        {
            var hotel = await hotelService.FindHotelByIdAsync(hotelId);
            var rooms = hotel.Rooms;

            if (rooms.Count == 0)
            {
                return NotFound();
            }

            return Ok(rooms) ;
        }

        [HttpPost]
        [Route("hotel")]
        public async Task<ActionResult> AddNewHotel([FromBody]AddHotelDTO hotel)
        {
            var viewHotel = mapper.Map<AddHotelDTO, HotelViewModel>(hotel);
            var newHotel = await hotelService.AddHotelAsync(viewHotel,hotel.UserID);

            return Ok("Success");
        }
    }
}