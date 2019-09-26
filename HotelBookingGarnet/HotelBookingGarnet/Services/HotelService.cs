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

        public async Task<Hotel> FindHotelByIdAsync(long Hotelid)
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
