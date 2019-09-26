using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;
using Microsoft.EntityFrameworkCore;

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

        public async Task EditHotelAsync(long HotelId, HotelViewModel editHotel)
        {
            var hotelToEdit = await FindHotelByIdAsync(HotelId);
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
                hotelToEdit.PropertyType = propertyTypeService.AddPropertyTypeAsync(editHotel.PropertyType);
            }
            applicationContext.Hotels.Update(hotelToEdit);
            await applicationContext.SaveChangesAsync();
        }

        public async Task<Hotel> FindHotelByIdAsync(long HotelId)
        {
            var foundHotel = await applicationContext.Hotels.SingleOrDefaultAsync(x => x.HotelId == HotelId);
            return foundHotel;
        }
    }
}