using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Services
{
    public class HotelService : IHotelService
    {
        private readonly ApplicationContext applicationContext;
        private readonly IPropertyTypeService propertyTypeService;

        public HotelService(ApplicationContext applicationContext, IPropertyTypeService propertyTypeService)
        {
            this.applicationContext = applicationContext;
            this.propertyTypeService = propertyTypeService;
        }

        public async Task AddHotelAsync(HotelViewModel newHotel, string userId)
        {
            var propertyType = await propertyTypeService.AddPropertyTypeAsync(newHotel.PropertyType);
            
            var hotel = new Hotel
            {
                HotelName = newHotel.HotelName,
                Country = newHotel.Country,
                Region = newHotel.Region,
                City = newHotel.City,
                Address = newHotel.Address,
                Description = newHotel.Description,
                StarRating = newHotel.StarRating,
                PropertyType = propertyType,
                Price = newHotel.Price,
                UserId = userId
            };
            
            propertyType.HotelPropertyTypes = new List<HotelPropertyType>();
            
            var smth = new HotelPropertyType();
            smth.Hotel = hotel;
            smth.HotelId = hotel.HotelId;
            smth.PropertyType = propertyType;
            smth.PropertyTypeId = propertyType.PropertyTypeId;
            
            propertyType.HotelPropertyTypes.Add(smth);

            await applicationContext.Hotels.AddAsync(hotel);
            await applicationContext.SaveChangesAsync();
        }
        
        
    }
}