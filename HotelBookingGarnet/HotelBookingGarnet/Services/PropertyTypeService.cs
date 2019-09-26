using System;
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
        
        public async Task<PropertyType> findPropertyTypeByTypeAsync(string PropertyTypeType)
        {
            var foundProperty = await applicationContext.PropertyTypes.FirstOrDefaultAsync(x => x.Type == PropertyTypeType);
            if (foundProperty == null)
            { 
                return null;
            }
            return foundProperty;
        }

        public async Task savePropertyType(string PropertyTypeType)
        {
            var foundProperty = await findPropertyTypeByTypeAsync(PropertyTypeType);
            if (foundProperty == null)
            {
                var newPropertyType = new PropertyType{Type = PropertyTypeType};
                applicationContext.SaveChangesAsync();
            }
        }
    }
}