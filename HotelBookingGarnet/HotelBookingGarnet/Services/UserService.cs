using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingGarnet.Services
{
    public class UserService : IUserService
    {
        private ApplicationContext application;

        public UserService(ApplicationContext application)
        {
            this.application = application;
        }

        public SelectList GetRolesFromDB()
        {
            return new SelectList(application.Roles.Where(u => u.Name.Contains("Admin")).ToList(), "Name", "Name");
        }
    }
}
