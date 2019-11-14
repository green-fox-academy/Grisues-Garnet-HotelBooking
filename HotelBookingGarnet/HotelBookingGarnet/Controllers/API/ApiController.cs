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

        
    }
}