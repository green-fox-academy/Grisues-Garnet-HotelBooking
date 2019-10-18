using AutoMapper;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Services.Helpers.AutoMapper.Profiles
{
    public class RoomFromRoomViewModel : Profile
    {
        public RoomFromRoomViewModel()
        {
            CreateMap<RoomViewModel, Room>();
        }
    }
}