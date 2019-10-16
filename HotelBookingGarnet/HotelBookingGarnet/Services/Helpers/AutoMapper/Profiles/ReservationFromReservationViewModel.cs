using AutoMapper;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Services.Helpers.AutoMapper.Profiles
{
    public class ReservationFromReservationViewModel : Profile
    {
        public ReservationFromReservationViewModel()
        {
            CreateMap<ReservationViewModel, Reservation>()
                .ForMember(
                    dest => dest.ReservationStart,
                    opt => opt.MapFrom(src => src.FromDate))
                .ForMember(
                    dest => dest.ReservationEnd,
                    opt => opt.MapFrom(src => src.ToDate))
                .ForMember(
                    dest => dest.NumberOfGuest,
                    opt => opt.MapFrom(src => src.NumberGuests));
        }
    }
}