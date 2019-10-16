using AutoMapper;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingGarnet.Services.Helpers.AutoMapper.Profiles
{
    public class ReservationFromApiReservationViewModel : Profile
    {
        public ReservationFromApiReservationViewModel()
        {
            CreateMap<ApiReservationViewModel, Reservation>()
                .ForMember(
                    dest => dest.HotelId,
                    opt => opt.MapFrom(src => src.HotelId))
                .ForMember(
                    dest => dest.ReservationStart,
                    opt => opt.MapFrom(src => src.FromDate))
                .ForMember(
                    dest => dest.ReservationEnd,
                    opt => opt.MapFrom(src => src.ToDate))
                .ForMember(
                    dest => dest.RoomId,
                    opt => opt.MapFrom(src => src.RoomId))
                .ForMember(
                    dest => dest.NumberOfGuest,
                    opt => opt.MapFrom(src => src.NumberGuests))
                .ForMember(
                    dest => dest.GuestsList,
                    opt => opt.MapFrom(src => src.GuestNames));
        }
    }
}