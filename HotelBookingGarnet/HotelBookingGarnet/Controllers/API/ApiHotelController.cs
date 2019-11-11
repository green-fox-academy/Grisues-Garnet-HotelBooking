using System.Threading.Tasks;
using AutoMapper;
using HotelBookingGarnet.DTOs;
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
        private readonly IMapper mapper;

        public ApiHotelController(IHotelService hotelService, IHotelPropertyTypeService hotelPropertyTypeService, IMapper mapper)
        {
            this.hotelService = hotelService;
            this.hotelPropertyTypeService = hotelPropertyTypeService;
            this.mapper = mapper;
        }

        [HttpPut("/api/hotel/{hotelId}")]
        public async Task<ActionResult> EditHotel([FromBody]AddHotelDTO hotelDto, [FromRoute] long hotelId)
        {
            var editHotel = mapper.Map<AddHotelDTO, HotelViewModel>(hotelDto);
            await hotelService.EditHotelAsync(hotelId, editHotel);
            await hotelPropertyTypeService.EditPropertyTypeAsync(hotelId, hotelDto.PropertyType);
            var hotel = await hotelService.FindHotelByIdAsync(hotelId);

            return Ok(hotel);
        }
    }
}