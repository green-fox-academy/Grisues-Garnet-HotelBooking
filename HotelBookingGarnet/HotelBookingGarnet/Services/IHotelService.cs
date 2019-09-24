using System.Threading.Tasks;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Services
{
    public interface IHotelService
    {
        Task AddHotelAsync(HotelViewModel newHotel, long UserId);
    }
}