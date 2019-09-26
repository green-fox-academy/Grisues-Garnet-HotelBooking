using System.Threading.Tasks;
using HotelBookingGarnet.Models;

namespace HotelBookingGarnet.Services
{
    public interface IPropertyTypeService
    {
        Task<PropertyType> findPropertyTypeByTypeAsync(string PropertyTypeType);
        Task savePropertyType(string PropertyTypeType);
    }
}