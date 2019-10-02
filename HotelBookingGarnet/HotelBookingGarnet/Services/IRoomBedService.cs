using System.Collections.Generic;
using HotelBookingGarnet.Models;

namespace HotelBookingGarnet.Services
{
    public interface IRoomBedService
    {
        List<RoomBed> GetRoomBeds();
    }
}