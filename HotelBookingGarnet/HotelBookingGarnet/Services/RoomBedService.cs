using System.Collections.Generic;
using System.Linq;
using HotelBookingGarnet.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingGarnet.Services
{
    public class RoomBedService : IRoomBedService
    {
        private readonly ApplicationContext applicationContext;

        public RoomBedService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public List<RoomBed> GetRoomBeds()
        {
            return applicationContext.RoomBed.Include(a => a.Bed).ToList();
        }
    }
}