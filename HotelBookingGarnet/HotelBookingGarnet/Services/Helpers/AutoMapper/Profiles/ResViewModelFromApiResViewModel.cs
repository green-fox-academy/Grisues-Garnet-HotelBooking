using AutoMapper;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Services.Helpers.AutoMapper.Profiles
{
    public class ResViewModelFromApiResViewModel : Profile
    {
        public ResViewModelFromApiResViewModel()
        {
            CreateMap<ApiReservationViewModel, ReservationViewModel>();
        }
    }
}