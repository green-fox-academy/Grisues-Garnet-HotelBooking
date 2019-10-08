using AutoMapper;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Services.Helpers.AutoMapper.Profiles
{
    public class HotelFromHotelViewModel : Profile
    {
        public HotelFromHotelViewModel()
        {
            CreateMap<HotelViewModel, Hotel>()
                .ForMember(
                    dest => dest.UserId,
                    opt => opt.UseDestinationValue());
        }
    }
}