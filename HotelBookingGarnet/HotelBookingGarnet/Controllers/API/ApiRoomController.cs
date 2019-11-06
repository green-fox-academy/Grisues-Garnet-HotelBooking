using System.Threading.Tasks;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingGarnet.Controllers.API
{
    [ApiController]
    public class ApiRoomController : ControllerBase
    {
        private readonly IRoomService roomService;

        public ApiRoomController(IRoomService roomService)
        {
            this.roomService = roomService;
        }

        [HttpPost("/api/addroom/{hotelId}")]
        public async Task<ActionResult> AddRoom([FromBody] RoomViewModel newRoom, [FromRoute] long hotelId)
        {
            await roomService.AddRoomAsync(newRoom, hotelId);
            return Ok();
        }
    }
}