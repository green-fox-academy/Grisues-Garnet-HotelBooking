using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingGarnet.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext applicationContext;

        public UserService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task<User> FindByUserIdAsync(long userId)
        {
            var user = await applicationContext.Users
                .Include(a => a.Hotels).FirstOrDefaultAsync(a => a.UserId == userId);
            if (user == null)
            {
                return null;
            }

            return user;
        }
    }
}