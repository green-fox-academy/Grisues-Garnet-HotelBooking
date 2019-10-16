using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;

namespace HotelBookingGarnet.Services
{
    public interface IApiHotelService
    {
        Task<List<Hotel>> FindHotelByCityAsync (string city);
    }
}