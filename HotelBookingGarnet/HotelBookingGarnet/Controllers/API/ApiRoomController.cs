using System.Threading.Tasks;
using AutoMapper;
using HotelBookingGarnet.DTOs;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingGarnet.Controllers.API
{
    [ApiController]
    public class ApiRoomController : ControllerBase
    {
        private readonly IRoomService roomService;
        private readonly IMapper mapper;

        public ApiRoomController(IRoomService roomService, IMapper mapper)
        {
            this.roomService = roomService;
            this.mapper = mapper;
        }
    

        [HttpPost("/api/room/{hotelId}")]
        public async Task<ActionResult> AddRoom([FromBody] RoomDTO newRoom, [FromRoute] long hotelId)
        {
            var newRoomView = mapper.Map<RoomDTO, RoomViewModel>(newRoom);
            await roomService.AddRoomAsync(newRoomView, hotelId);
            return Ok();
        }
    }
}