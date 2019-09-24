using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Services
{
    public interface IHotelService
    { 
        Task editHotelAsync(long HotelId, HotelViewModel editHotel);
        Task<Hotel> findHotelByIdAsync(long HotelId);
    }
}