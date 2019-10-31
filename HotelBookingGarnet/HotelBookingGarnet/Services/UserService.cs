using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Mailgun;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HotelBookingGarnet.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext applicationContext;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private string domainName;
        private string apiKey;

        public UserService(ApplicationContext applicationContext, UserManager<User> UserManager,
            SignInManager<User> SignInManager, IConfiguration configuration)
        {
            this.applicationContext = applicationContext;
            userManager = UserManager;
            signInManager = SignInManager;
            domainName = configuration.GetConnectionString("DomainName");
            apiKey = configuration.GetConnectionString("ApiKey");
        }

        public async Task<User> FindUserByHotelIdAsync(long hotelId)
        {
            var hotel = await applicationContext.Hotels
                .FirstOrDefaultAsync(a => a.HotelId == hotelId);

            var user = await applicationContext.Users
                .FirstOrDefaultAsync(a => a.Id == hotel.UserId);
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

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public AuthenticationProperties ConfigureExternalAutheticationProp(string provider, string returnUrl)
        {
            return signInManager.ConfigureExternalAuthenticationProperties(provider, returnUrl);
        }

        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            return await signInManager.GetExternalLoginInfoAsync();
        }

        public async Task<SignInResult> ExternalLoginSingnInAsync(string loginProvider, string providerKey,
            bool isPersistent)
        {
            return await signInManager.ExternalLoginSignInAsync(loginProvider, providerKey, isPersistent);
        }

        public async Task<List<string>> CreateAndLoginGoogleUserAsync(ExternalLoginInfo info)
        {
            var user = await userManager.FindByEmailAsync(info.Principal.FindFirstValue(ClaimTypes.Email));

            if (user == null)
            {
                user = new User
                {
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = info.Principal.FindFirst(ClaimTypes.Email).Value.Split("@")[0],
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Guest");
                }
            }

            var loginResult = await userManager.AddLoginAsync(user, info);

            if (loginResult.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
            }

            return loginResult.Errors
                .Select(e => e.Description)
                .ToList();
        }

        public async Task<List<string>> IsEmailPresent(LoginViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                model.ErrorMessages.Add("User with the given Email does not exist.");
                return model.ErrorMessages;
            }

            return model.ErrorMessages;
        }

        public string GenerateNewPassword()
        {
            var random = new Random();
            var length = random.Next(12, 20);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var generatedPassword = "";

            while (!generatedPassword.Any(char.IsUpper) || !generatedPassword.Any(char.IsDigit))
            {
                generatedPassword = Generation(chars, length, random);
            }

            return generatedPassword;
        }

        private static string Generation(string chars, int length, Random random)
        {
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task ChangePasswordAsync(string newRandomPassword, User user)
        {
            await userManager.RemovePasswordAsync(user);
            await userManager.AddPasswordAsync(user, newRandomPassword);
        }

        public async Task SendRecoveryPasswordAsync(string modelEmail, string newPassword)
        {
            var sender = new MailgunSender(domainName, apiKey);
            Email.DefaultSender = sender;

            var template =
                "You received this message because you reset your password in the Garnet Travel " +
                "system. \r\n\n Email address: " + modelEmail +
                "\r\n Your new password: " + newPassword +
                "\r\n\n Sincerely, Garnet Travel Team" +
                "\r\n (This is an auto generated message, please do not reply!)";

            var email = Email
                .From("mailgun@sandbox0ec3cdedf8584e3fa03c7b70b98fc52f.mailgun.org", "GarnetTravel.Support")
                .To(modelEmail)
                .Subject($"You reset your password")
                .UsingTemplate(template, false, false);

            await email.SendAsync();
        }

        public async Task<User> FindUserByTaxiReservationIdAsync(string userId)
        {
            var user = await applicationContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }
    }
}