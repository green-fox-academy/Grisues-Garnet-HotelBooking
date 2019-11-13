using AutoMapper;
using HotelBookingGarnet.DTO;
using HotelBookingGarnet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingGarnet.Services.Helpers.AutoMapper.Profiles
{
    public class HotelViewModelFromAddHotelDTO : Profile
    {
        public HotelViewModelFromAddHotelDTO()
        {
            CreateMap<AddHotelDTO,HotelViewModel>();
        }
    }
}
