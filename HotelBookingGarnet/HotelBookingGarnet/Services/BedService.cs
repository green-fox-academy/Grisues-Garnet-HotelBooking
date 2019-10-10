using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Services
{
    public class BedService : IBedService
    {
        private readonly ApplicationContext applicationContext;
        private readonly IRoomService roomService;
        private readonly IMapper mapper;

        public BedService(ApplicationContext applicationContext, IRoomService roomService, IMapper mapper)
        {
            this.applicationContext = applicationContext;
            this.roomService = roomService;
            this.mapper = mapper;
        }

        public async Task AddBedAsync(BedViewModel newBed, long roomId)
        {
            var room = await roomService.FindRoomByIdAsync(roomId);
            var bed = mapper.Map<BedViewModel, Bed>(newBed);
            await applicationContext.AddAsync(bed);
            await applicationContext.SaveChangesAsync();

            var roomBed = new RoomBed
            {
                Room = room,
                RoomId = roomId,
                Bed = bed,
                BedId = bed.BedId
            };
            room.RoomBeds.Add(roomBed);

            await applicationContext.SaveChangesAsync();
        }
    }
}