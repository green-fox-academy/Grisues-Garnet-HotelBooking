using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingGarnet.Services
{
    public class ApiHotelService : IApiHotelService
    {
        private readonly ApplicationContext applicationContext;
        
        public ApiHotelService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }
        
        public async Task<List<Hotel>> FindHotelByCityAsync(string city)
        {
             var hotels = await applicationContext.Hotels.Include(h => h.Rooms)
                .Where(h => h.City.Contains(city) || String.IsNullOrEmpty(city))
                .Where(h => h.IsItAvailable)
                .OrderBy(h => h.HotelName).ToListAsync();
             
             return hotels;
        }
    }
}