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
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserService(ApplicationContext applicationContext, UserManager<User> UserManager,
            SignInManager<User> SignInManager)
        {
            this.applicationContext = applicationContext;
            userManager = UserManager;
            signInManager = SignInManager;
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
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await AddUserToRoleAsync(user, model);
                return result;
            }
            return result;
        }

        public async Task<List<string>> LoginAsync(LoginViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                model.ErrorMessages.Add("User with the given Email does not exist");
                return model.ErrorMessages;
            }

            var result = await signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false,
                lockoutOnFailure: false);
            model.ErrorMessages = CheckLoginErrors(result, model.ErrorMessages);
            return model.ErrorMessages;
        }

        private List<string> CheckLoginErrors(SignInResult result, List<string> errors)
        {
            if (!result.Succeeded)
            {
                errors.Add("Invalid login attempt");
            }

            return errors;
        }

        public async Task AddUserToRoleAsync(User user, RegisterViewModel model)
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

        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

//        public bool ValidatePasswordChangingFields(string OldPwd, string NewPwd, string ConfirmPwd)
//        {
//            if (OldPwd != NewPwd && OldPwd != null & NewPwd != null && ConfirmPwd != null && NewPwd == ConfirmPwd)
//            {
//                return true;
//            }
//
//            return false;
//        }
//
//        public Task<IdentityResult> ChangePasswordAsync(SettingsViewModel settingsViewModel)
//        {
//            var validatedFields = ValidatePasswordChangingFields(settingsViewModel.OldPassword,
//                settingsViewModel.NewPassword, settingsViewModel.ConfirmPassword);
//            if (validatedFields)
//            {
//                var result = userManager.ChangePasswordAsync(settingsViewModel.User, settingsViewModel.OldPassword,
//                    settingsViewModel.NewPassword);
//                return result;
//            }
//            
//        }
    }
}