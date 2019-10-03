using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using Microsoft.EntityFrameworkCore;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Services
{
    public class HotelService : IHotelService
    {
        private readonly ApplicationContext applicationContext;
        private readonly IPropertyTypeService propertyTypeService;
        private readonly IImageService imageService;

        public HotelService(ApplicationContext applicationContext, IPropertyTypeService propertyTypeService,
            IImageService imageService)
        {
            this.applicationContext = applicationContext;
            this.propertyTypeService = propertyTypeService;
            this.imageService = imageService;
        }

        public async Task EditHotelAsync(long HotelId, HotelViewModel editHotel)
        {
            var hotelToEdit = await FindHotelByIdAsync(HotelId);
            var property = await propertyTypeService.AddPropertyTypeAsync(editHotel.PropertyType);
            if (hotelToEdit != null)
            {
                hotelToEdit.HotelName = editHotel.HotelName;
                hotelToEdit.Country = editHotel.Country;
                hotelToEdit.Region = editHotel.Region;
                hotelToEdit.City = editHotel.City;
                hotelToEdit.Address = editHotel.Address;
                hotelToEdit.Description = editHotel.Description;
                hotelToEdit.StarRating = editHotel.StarRating;
                hotelToEdit.Price = editHotel.Price;
            }

            applicationContext.Hotels.Update(hotelToEdit);
            await applicationContext.SaveChangesAsync();
        }

        public async Task<Hotel> FindHotelByIdAsync(long HotelId)
        {
            var foundHotel = await applicationContext.Hotels.Include(p => p.HotelPropertyTypes)
                .Include(h => h.Rooms).SingleOrDefaultAsync(x => x.HotelId == HotelId);

            return foundHotel;
        }

        public async Task<long> AddHotelAsync(HotelViewModel newHotel, string userId)
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
                Price = newHotel.Price,
                UserId = userId
                
            };

            await applicationContext.Hotels.AddAsync(hotel);
            await applicationContext.SaveChangesAsync();

            propertyType.HotelPropertyTypes = new List<HotelPropertyType>();

            var hotelPropertyType = new HotelPropertyType();
            hotelPropertyType.Hotel = hotel;
            hotelPropertyType.HotelId = hotel.HotelId;
            hotelPropertyType.PropertyType = propertyType;
            hotelPropertyType.PropertyTypeId = propertyType.PropertyTypeId;

            propertyType.HotelPropertyTypes.Add(hotelPropertyType);

            await applicationContext.SaveChangesAsync();
            return hotel.HotelId;
        }

        public List<Hotel> GetHotels()
        {
            var qry = applicationContext.Hotels.AsQueryable().OrderBy(h => h.HotelName).ToList();
            return qry;
        }

        public async Task<Hotel> FindHotelByName(string hotelName)
        {
            var foundedHotel =await applicationContext.Hotels.Include(a => a.HotelPropertyTypes)
                .FirstOrDefaultAsync(a => a.HotelName == hotelName);
            return foundedHotel;
        }

        public async Task FindHotelThumb()
        {
            var hotels = GetHotels();
            foreach (var hotel in hotels)
            {
                var images = await imageService.ListAsync(hotel.HotelId);
                hotel.Uri = images[0].Path;
            }
        }
    }
}