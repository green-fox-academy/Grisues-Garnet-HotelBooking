using HotelBookingGarnet.Controllers.Login;
using HotelBookingGarnet.Models;
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
        private ApplicationContext application;

        public RegisterController(UserManager<User> userManager,ApplicationContext applicationContext)
        {
            _userManager = userManager;
            this.application = applicationContext;

        }

        [HttpGet("/register")]
        [AllowAnonymous]
        public IActionResult Register()
        {
            ViewBag.Name = new SelectList(application.Roles.Where(u => !u.Name.Contains("Admin")).ToList(), "Name", "Name");
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
                    await _userManager.AddToRoleAsync(user, model.UserRoles);
                    return RedirectToAction(nameof(LoginController.Login), "Login");
                }
            }
            return View(model);
        }
    }

}
