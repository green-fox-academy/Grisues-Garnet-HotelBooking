using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingGarnet.Services
{
    public interface IUserService
    {
       Task AddUserToRole(User user,RegisterViewModel model);
    }
}
