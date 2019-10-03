using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingGarnet.Services
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationContext applicationContext;

        public RoomService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }
        
        public async Task AddRoomAsync(RoomViewModel newRoom, long hotelId)
        {
            var room = new Room
            {
                RoomName = newRoom.RoomName,
                Price = newRoom.Price,
                NumberOfGuests = newRoom.NumberOfGuests,
                NumberOfRooms = newRoom.NumberOfRooms,
                HotelId = hotelId,
                NumberOfAvailablePlaces = newRoom.NumberOfGuests * newRoom.NumberOfRooms
            };
            await applicationContext.Rooms.AddAsync(room);
            await applicationContext.SaveChangesAsync();
        }

        public async Task<Room> FindRoomByIdAsync(long roomId)
        {
            var room = await applicationContext.Rooms.Include(a => a.RoomBeds).FirstOrDefaultAsync(a => a.RoomId == roomId);
            return room;
        }

        public async Task<List<Room>> FindAvailableRoomsAsync()
        {
            var rooms = await applicationContext.Rooms.Where(r => r.NumberOfRooms != null).ToListAsync();
            return rooms;
        }
    }
}