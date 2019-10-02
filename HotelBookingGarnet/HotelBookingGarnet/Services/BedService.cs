using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Services
{
    public class BedService : IBedService
    {
        private readonly ApplicationContext applicationContext;
        private readonly IRoomService roomService;

        public BedService(ApplicationContext applicationContext, IRoomService roomService)
        {
            this.applicationContext = applicationContext;
            this.roomService = roomService;
        }

        public async Task AddBedAsync(BedViewModel newBed, long roomId)
        {
            var room = await roomService.FindRoomByIdAsync(roomId);
            var bed = new Bed
            {
                BedType = newBed.BedType,
                NumberOfBeds = newBed.NumberOfBeds
            };
            
            await applicationContext.AddAsync(bed);
            await applicationContext.SaveChangesAsync();
            
            room.RoomBeds = new List<RoomBed>();

            var connection = new RoomBed();
            connection.Room = room;
            connection.RoomId = roomId;
            connection.Bed = bed;
            connection.BedId = bed.BedId;
            room.RoomBeds.Add(connection);
            
            await applicationContext.SaveChangesAsync();
        }
    }
}