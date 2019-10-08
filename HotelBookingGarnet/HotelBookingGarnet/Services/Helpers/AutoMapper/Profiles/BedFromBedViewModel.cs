using AutoMapper;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Services.Helpers.AutoMapper.Profiles
{
    public class BedFromBedViewModel : Profile
    {
        public BedFromBedViewModel()
        {
            CreateMap<BedViewModel, Bed>();
        }
    }
}