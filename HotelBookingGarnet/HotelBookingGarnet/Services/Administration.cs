using HotelBookingGarnet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingGarnet.Services
{
    public class Administration
    {

        public static void CreateRoles(UserManager<User> userManager)
        {

            if (userManager.FindByEmailAsync("admin@gmail.com").Result == null)
            {
                User user = new User
                {
                    UserName = "Admin",
                    Email = "admin@gmail.com",
                };

             
                IdentityResult check = userManager.CreateAsync(user, "Password1234..").Result;

                if (check.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }

                var userCurrent = userManager.FindByEmailAsync("admin@gmail.com").Result;

                if (!userManager.IsInRoleAsync(userCurrent, "Admin").Result)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();

                }
            }

        }   

    }
}
