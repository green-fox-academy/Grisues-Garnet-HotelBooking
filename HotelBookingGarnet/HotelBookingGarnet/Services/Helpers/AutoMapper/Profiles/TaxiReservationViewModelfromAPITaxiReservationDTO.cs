using AutoMapper;
using HotelBookingGarnet.DTOs;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingGarnet.Services.Helpers.AutoMapper.Profiles
{
    public class TaxiReservationViewModelfromAPITaxiReservationDTO : Profile
    {
        public TaxiReservationViewModelfromAPITaxiReservationDTO()
        {
            CreateMap<APITaxiReservationDTO, TaxiReservationViewModel>();
        }
    }
}
