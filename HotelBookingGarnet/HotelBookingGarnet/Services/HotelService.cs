using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingGarnet.Services
{
    public class HotelService :IHotelService
    {
        private readonly ApplicationContext applicationContext;

        public HotelService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task<List<Hotel>> findAllHotelAsync()
        {
            return await applicationContext.Hotels.ToListAsync();
        }

        public async Task<Hotel> findHotelByIdAsync(long Hotelid)
        {
            
            return await applicationContext.Hotels.SingleOrDefaultAsync(h => h.HotelId == Hotelid);
           
        }

        public List<Hotel> GetHotels()
        {
            var qry = applicationContext.Hotels.AsNoTracking().OrderBy(h => h.HotelName).ToList();
            return qry;
        }
    }
}
