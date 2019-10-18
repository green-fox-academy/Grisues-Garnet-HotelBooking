using AutoMapper;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Services.Helpers.AutoMapper.Profiles
{
    public class ReservationFromReservationViewModel : Profile
    {
        public ReservationFromReservationViewModel()
        {
            CreateMap<ReservationViewModel, Reservation>();
        }
    }
}