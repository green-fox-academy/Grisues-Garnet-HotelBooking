using AutoMapper;
using HotelBookingGarnet.DTOs;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Services.Helpers.AutoMapper.Profiles
{
    public class RoomViewModelFromRoomDTO : Profile
    {
        public RoomViewModelFromRoomDTO()
        {
            CreateMap<RoomDTO, RoomViewModel>();
        }
    }
}