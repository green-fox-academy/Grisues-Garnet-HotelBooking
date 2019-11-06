using System.Threading.Tasks;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingGarnet.Controllers.API
{
    [ApiController]
    public class ApiHotelController : ControllerBase
    {
        private readonly IHotelService hotelService;
        private readonly IHotelPropertyTypeService hotelPropertyTypeService;

        public ApiHotelController(IHotelService hotelService, IHotelPropertyTypeService hotelPropertyTypeService)
        {
            this.hotelService = hotelService;
            this.hotelPropertyTypeService = hotelPropertyTypeService;
        }

        [HttpPut("/api/edithotel/{hotelId}")]
        public async Task<ActionResult> EditHotel([FromBody]HotelViewModel editHotel, [FromRoute] long hotelId)
        {
            await hotelService.EditHotelAsync(hotelId, editHotel);
            await hotelPropertyTypeService.EditPropertyTypeAsync(hotelId, editHotel.PropertyType);
            var hotel = await hotelService.FindHotelByIdAsync(hotelId);

            return Ok(hotel);
        }
    }
}