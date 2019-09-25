using System;
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

        public async Task<User> FindByEmailAsync(string email)
        {
            var user = await applicationContext.Users
                .Include(a => a.Hotels).FirstOrDefaultAsync(a => a.Email == email);
            if (user == null)
            {
                return null;
            }

            return user;
        }
    }
}