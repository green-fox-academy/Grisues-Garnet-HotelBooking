using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingGarnet.Services
{
    public class HotelService : IHotelService
    {
        private readonly ApplicationContext applicationContext;

        public HotelService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task editHotelAsync(long HotelId, HotelViewModel editHotel)
        {
            var hotelToEdit = await findHotelByIdAsync(HotelId);
            if (hotelToEdit != null)
            {

                hotelToEdit.HotelName = editHotel.HotelName;
                hotelToEdit.Country = editHotel.Country;
                hotelToEdit.Region = editHotel.Region;
                hotelToEdit.City = editHotel.City;
                hotelToEdit.Address = editHotel.Address;
                hotelToEdit.Description = editHotel.Description;
                hotelToEdit.StarRating = (int) editHotel.StarRating;
                hotelToEdit.PropertyType = editHotel.PropertyType;
                hotelToEdit.Price = editHotel.Price;
            }

            applicationContext.Hotels.Update(hotelToEdit);
            await applicationContext.SaveChangesAsync();
        }

        public async Task<Hotel> findHotelByIdAsync(long HotelId)
        {
            var foundHotel = await applicationContext.Hotels.SingleOrDefaultAsync(x => x.HotelId == HotelId);
            return foundHotel;
        }
    }
}