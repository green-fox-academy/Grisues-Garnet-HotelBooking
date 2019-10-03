using System.Threading.Tasks;
using HotelBookingGarnet.Models;

namespace HotelBookingGarnet.Services
{
    public interface IPropertyTypeService
    {
        Task<PropertyType> FindByTypeAsync(string type);
        Task<PropertyType> AddPropertyTypeAsync(string type);
    }
}