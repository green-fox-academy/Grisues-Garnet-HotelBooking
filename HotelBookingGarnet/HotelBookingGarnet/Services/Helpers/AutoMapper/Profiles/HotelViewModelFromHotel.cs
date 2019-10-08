using AutoMapper;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Services.Helpers.AutoMapper.Profiles
{
    public class HotelViewModelFromHotel : Profile
    {
        public HotelViewModelFromHotel()
        {
            CreateMap<Hotel, HotelViewModel>();
        }
    }
}