using System.Linq;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingGarnet.Services
{
    public class PropertyTypeService : IPropertyTypeService
    {
        private readonly ApplicationContext applicationContext;

        public PropertyTypeService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }
        
        public async Task<PropertyType> FindByTypeAsync(string type)
        {
            var propertyType = await applicationContext.PropertyTypes.Include(a => a.HotelPropertyTypes).FirstOrDefaultAsync(a => a.Type == type);
            if (propertyType == null)
            {
                return null;
            }
            return propertyType;
        }

        public async Task<PropertyType> AddPropertyTypeAsync(string type)
        {
            var findPropertyType = await FindByTypeAsync(type);

            if (findPropertyType == null)
            {
                var propertyType = new PropertyType
                {
                    Type = type
                };
                await applicationContext.PropertyTypes.AddAsync(propertyType);
                await applicationContext.SaveChangesAsync();
                return propertyType;
            }
            return findPropertyType;
        }
    }
}