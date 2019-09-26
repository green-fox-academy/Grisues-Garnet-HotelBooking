using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Services
{
    public interface IHotelService
    { 
        Task EditHotelAsync(long HotelId, HotelViewModel editHotel);
        Task<Hotel> FindHotelByIdAsync(long HotelId);
    }
}