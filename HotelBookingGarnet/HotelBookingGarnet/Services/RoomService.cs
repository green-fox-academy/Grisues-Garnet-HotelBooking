using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingGarnet.Services
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationContext applicationContext;
        private readonly IHotelService hotelService;
        private readonly IMapper mapper;

        public RoomService(ApplicationContext applicationContext, IHotelService hotelService, IMapper mapper)
        {
            this.applicationContext = applicationContext;
            this.hotelService = hotelService;
            this.mapper = mapper;
        }

        public async Task AddRoomAsync(RoomViewModel newRoom, long hotelId)
        {
            var room = mapper.Map<RoomViewModel, Room>(newRoom);
            room.HotelId = hotelId;
            room.NumberOfAvailablePlaces = newRoom.NumberOfGuests * newRoom.NumberOfRooms;
            
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
            var room = await applicationContext.Rooms.Include(a => a.RoomBeds)
                .FirstOrDefaultAsync(a => a.RoomId == roomId);
            return room;
        }
    }
}