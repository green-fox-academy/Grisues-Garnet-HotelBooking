using HotelBookingGarnet;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingGarnetTest.TestUtils
{
    public class TestDbOptions
    {
        public static DbContextOptions<ApplicationContext> Get()
        {
            return new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "hotel-testdb")
                .Options;
        }
    }
}