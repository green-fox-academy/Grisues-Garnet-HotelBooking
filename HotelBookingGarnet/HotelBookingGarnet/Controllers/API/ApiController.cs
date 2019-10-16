using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.Utils;
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
        
        [HttpGet("/api/hotels?city={city}")]
        public IActionResult Get([FromQuery(Name = "city")] string city)
        {


            return null;
        }
    }
}