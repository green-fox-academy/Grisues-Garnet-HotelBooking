using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingGarnet.Services
{
    public class HotelPropertyTypeService : IHotelPropertyTypeService
    {
        private readonly ApplicationContext applicationContext;
        private readonly IPropertyTypeService propertyTypeService;
        private readonly IHotelService hotelService;

        public HotelPropertyTypeService(ApplicationContext applicationContext, IPropertyTypeService propertyTypeService, IHotelService hotelService)
        {
            this.applicationContext = applicationContext;
            this.propertyTypeService = propertyTypeService;
            this.hotelService = hotelService;
        }

        public async Task<HotelPropertyType> FindPropertyByHotelIdAsync(long hotelId)
        {
            var hotelPropertyType = await applicationContext.HotelPropertyType.Include(a => a.PropertyType)
                .Include(a => a.Hotel).FirstOrDefaultAsync(a => a.HotelId == hotelId);

            return hotelPropertyType;
        }

        public async Task EditPropertyTypeAsync(long hotelId, string newPropertyType)
        {
            var hotelPropertyType = await FindPropertyByHotelIdAsync(hotelId);
            var hotel = await hotelService.FindHotelByIdAsync(hotelId);

            if (newPropertyType != hotelPropertyType.PropertyType.Type)
            {
                var propertyType = await propertyTypeService.FindByTypeAsync(newPropertyType);

                if (propertyType == null)
                {
                    var prop = await propertyTypeService.AddPropertyTypeAsync(newPropertyType);

                    applicationContext.Remove(hotelPropertyType);
                    await applicationContext.SaveChangesAsync();

                    var newHotelPropertyType = new HotelPropertyType();
                    newHotelPropertyType.PropertyType = prop;
                    newHotelPropertyType.PropertyTypeId = prop.PropertyTypeId;
                    newHotelPropertyType.Hotel = hotel;
                    newHotelPropertyType.HotelId = hotelId;
                    hotel.HotelPropertyTypes.Add(newHotelPropertyType);
                    await applicationContext.SaveChangesAsync();
                }
                else
                {
                    applicationContext.Remove(hotelPropertyType);
                    await applicationContext.SaveChangesAsync();
                    
                    var newHotelPropertyType = new HotelPropertyType();
                    newHotelPropertyType.PropertyType = propertyType;
                    newHotelPropertyType.PropertyTypeId = propertyType.PropertyTypeId;
                    newHotelPropertyType.Hotel = hotel;
                    newHotelPropertyType.HotelId = hotelId;
                    hotel.HotelPropertyTypes.Add(newHotelPropertyType);
                    await applicationContext.SaveChangesAsync();
                }
            }
        }
    }
}