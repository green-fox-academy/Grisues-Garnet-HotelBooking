using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Services
{
    public class HotelService : IHotelService
    {
        private readonly ApplicationContext applicationContext;

        public HotelService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task AddHotelAsync(HotelViewModel newHotel, long userId)
        {
            var hotel = new Hotel
            {
                HotelName = newHotel.HotelName,
                Country = newHotel.Country,
                Region = newHotel.Region,
                City = newHotel.City,
                Address = newHotel.Address,
                Description = newHotel.Description,
                StarRating = newHotel.StarRating,
                PropertyType = newHotel.PropertyType,
                Price = newHotel.Price,
                UserId = userId
            };
            await applicationContext.Hotels.AddAsync(hotel);
            await applicationContext.SaveChangesAsync();
        }
    }
}