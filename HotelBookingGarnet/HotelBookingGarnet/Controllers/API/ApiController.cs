using System.Threading.Tasks;
using HotelBookingGarnet.Services;
using Microsoft.AspNetCore.Mvc;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Controllers.API
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        
        private readonly IHotelService hotelService;
        private readonly IApiHotelService apiHotelService;

        public ApiController(IHotelService hotelService, IApiHotelService apiHotelService)
        {
            this.hotelService = hotelService;
            this.apiHotelService = apiHotelService;
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
            {
                return model;
            }
            
            model.HotelList = await apiHotelService.FindHotelByCityAsync(city);
            return model;
        }
    }
}