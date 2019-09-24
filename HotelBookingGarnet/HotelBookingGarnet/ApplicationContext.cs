using Microsoft.EntityFrameworkCore;

namespace HotelBookingGarnet
{
    public class ApplicationContext : DbContext
    {
      

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
    }
}