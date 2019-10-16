using AutoMapper;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Services.Helpers.AutoMapper.Profiles
{
    public class UserFromRegistrationViewModel : Profile
    {
        public UserFromRegistrationViewModel()
        {
            CreateMap<RegisterViewModel, User>();
        }
    }
}