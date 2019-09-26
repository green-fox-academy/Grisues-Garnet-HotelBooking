using HotelBookingGarnet.Controllers.Login;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingGarnet.Controllers.Register
{
    public class RegisterController:Controller
    {
        private readonly UserManager<User> _userManager;
        private IUserService userService;

        public RegisterController(UserManager<User> userManager, IUserService userService)
        {
            _userManager = userManager;
            this.userService = userService;
        }

        [HttpGet("/register")]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("/register")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Username, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await userService.AddUserToRole(user, model);
                    return RedirectToAction(nameof(LoginController.Login), "Login");
                }
            }
            return View(model);
        }
    }

}
