﻿using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace HotelBookingGarnet.Services
{
    public interface IUserService
    {
        Task AddUserToRoleAsync(User user, RegisterViewModel model);
        Task<List<string>> LoginAsync(LoginViewModel model);
        Task<IdentityResult> RegisterAsync(RegisterViewModel model);
        Task Logout();
        Task<User> FindUserByHotelIdAsync(long hotelId);
        AuthenticationProperties ConfigureExternalAutheticationProp(string provider, string returnUrl);
        Task<ExternalLoginInfo> GetExternalLoginInfoAsync();
        Task<SignInResult> ExternalLoginSingnInAsync(string loginProvider, string providerKey, bool isPersistent);
        Task<List<string>> CreateAndLoginGoogleUserAsync(ExternalLoginInfo info);
    }
}