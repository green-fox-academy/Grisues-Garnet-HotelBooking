using AutoMapper;
using HotelBookingGarnet.DTO;
using HotelBookingGarnet.ViewModels;

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