using AutoMapper;
using HotelBookingGarnet.DTOs;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Services.Helpers.AutoMapper.Profiles
{
    public class HotelViewModelFromAddHotelDTO : Profile
    {
        public HotelViewModelFromAddHotelDTO()
        {
            CreateMap<AddHotelDTO, HotelViewModel>();
        }
    }
}