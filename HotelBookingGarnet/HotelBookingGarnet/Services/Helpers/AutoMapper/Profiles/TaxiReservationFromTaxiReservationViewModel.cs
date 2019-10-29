using AutoMapper;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Services.Helpers.AutoMapper.Profiles
{
    public class TaxiReservationFromTaxiReservationViewModel : Profile
    {
        public TaxiReservationFromTaxiReservationViewModel()
        {
            CreateMap<TaxiReservationViewModel, TaxiReservation>();
        }
    }
}

