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
        }   
    }
}