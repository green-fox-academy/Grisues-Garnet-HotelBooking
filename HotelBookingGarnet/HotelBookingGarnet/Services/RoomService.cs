using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingGarnet.Services
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationContext applicationContext;
        private readonly IHotelService hotelService;

        public RoomService(ApplicationContext applicationContext, IHotelService hotelService)
        {
            this.applicationContext = applicationContext;
            this.hotelService = hotelService;
        }

        public async Task AddRoomAsync(RoomViewModel newRoom, long hotelId)
        {
            var room = new Room
            {
                RoomName = newRoom.RoomName,
                Price = newRoom.Price,
                NumberOfGuests = newRoom.NumberOfGuests,
                NumberOfRooms = newRoom.NumberOfRooms,
                HotelId = hotelId
            };

            var hotel = await hotelService.FindHotelByIdAsync(hotelId);
            if (hotel.Price > room.Price || hotel.Price == 0)
            {
                hotel.Price = room.Price;
            }

            applicationContext.Hotels.Update(hotel);
            await applicationContext.Rooms.AddAsync(room);
            await applicationContext.SaveChangesAsync();
        }

        public async Task<Room> FindRoomByIdAsync(long roomId)
        {
            var room = await applicationContext.Rooms.Include(a => a.RoomBeds).FirstOrDefaultAsync(a => a.RoomId == roomId);
            return room;
        }
    }
}