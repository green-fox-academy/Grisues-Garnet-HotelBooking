using HotelBookingGarnet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingGarnet.Services
{
    public class Administrator
    {
        public static void CreateAdmin(UserManager<User> userManager)
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
            }

            if (userManager.FindByEmailAsync("g@gmail.com").Result == null)
            {
                User user = new User
                {
                    UserName = "Guest",
                    Email = "g@gmail.com",
                };

                IdentityResult check = userManager.CreateAsync(user, "Guest1").Result;

                if (check.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Guest").Wait();
                }
            }

            if (userManager.FindByEmailAsync("m@gmail.com").Result == null)
            {
                User user = new User
                {
                    UserName = "Manager",
                    Email = "m@gmail.com",
                };

                IdentityResult check = userManager.CreateAsync(user, "Manager1").Result;

                if (check.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Hotel Manager").Wait();
                }
            }
        }
    }
}