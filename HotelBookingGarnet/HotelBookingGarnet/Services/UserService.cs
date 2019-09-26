using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingGarnet.Services
{
    public class UserService:IUserService
    {
        private readonly UserManager<User> userManager;

        public UserService(UserManager<User> UserManager)
        {
            userManager = UserManager;
        }


        public async Task AddUserToRole(User user,RegisterViewModel model)
        {
            if (model.IsManager)
            {
                await userManager.AddToRoleAsync(user, "Hotel Manager");
            }
            else
            {
                await userManager.AddToRoleAsync(user, "Guest");
            }
                


        }

    }
}
