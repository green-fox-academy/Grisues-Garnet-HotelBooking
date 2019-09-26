using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingGarnet.Controllers.Home;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingGarnet.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext applicationContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        public UserService(ApplicationContext applicationContext, UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            this.applicationContext = applicationContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            var user = await applicationContext.Users
                .Include(a => a.Hotels).FirstOrDefaultAsync(a => a.Email == email);
            return user;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {
            var user = new User {UserName = model.Username, Email = model.Email};
            var result = await _userManager.CreateAsync(user, model.Password);
            return result;
        }
        
        public async Task<List<string>> LoginAsync(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                model.ErrorMessages.Add("User with the given Email does not exist");
                return model.ErrorMessages;
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);
            model.ErrorMessages = checkLoginErrors(result, model.ErrorMessages);
            return model.ErrorMessages;
        }
        
        private List<string> checkLoginErrors(SignInResult result, List<string> errors)
        {
            if (!result.Succeeded)
            {
                errors.Add("Invalid login attempt");
            }
            return errors;
        }
    }
}