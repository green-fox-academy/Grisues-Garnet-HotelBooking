using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Services
{
    public interface IHotelPropertyTypeService
    {
        Task<HotelPropertyType> FindPropertyByHotelIdAsync(long hotelId);
        Task EditPropertyTypeAsync(long hotelId, string newPropertyType);
    }
}