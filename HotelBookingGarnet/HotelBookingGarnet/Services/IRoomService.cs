using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Services
{
    public interface IRoomService
    {
        Task AddRoomAsync(RoomViewModel newRoom, long hotelId);
        Task<Room> FindRoomByIdAsync(long roomId);
    }
}