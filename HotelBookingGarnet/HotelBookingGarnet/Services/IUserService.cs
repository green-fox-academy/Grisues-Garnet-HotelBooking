using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace HotelBookingGarnet.Services
{
    public interface IUserService
    {
        Task AddUserToRoleAsync(User user, RegisterViewModel model);
        Task<User> FindByEmailAsync(string email);
        Task<List<string>> LoginAsync(LoginViewModel model);
        Task<IdentityResult> RegisterAsync(RegisterViewModel model);
        Task Logout();
        Task<User> FindUserByHotelIdAsync(long hotelId);
    }
}