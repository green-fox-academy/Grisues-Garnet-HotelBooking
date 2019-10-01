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
            var propertType = await applicationContext.PropertyTypes.FirstOrDefaultAsync(a => a.Type == type);
            if (propertType == null)
            {
                return null;
            }

            return propertType;
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

        public async Task<string> FindByIdAsync(long id)
        {
            var property = await applicationContext.PropertyTypes.FirstOrDefaultAsync(p => p.PropertyTypeId == id);
            return property.Type;
        }
    }
}