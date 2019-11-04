using HotelBookingGarnet.Controllers.Register;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotelBookingGarnetTest.Controllers
{
    public class RegisterControllerTest
    {
        private readonly Mock<IUserService> mockUserService;

        public RegisterControllerTest()
        {
            mockUserService = new Mock<IUserService>();
        }

        [Fact]
        public async Task Register_UserShouldRegisterAndRedirectLogin()
        {
            var model = new RegisterViewModel {
                Email = "guest@gmail.com",
                Username = "guest",
                Password = "Alma123",
                ConfirmPassword = "Alma123"
            };

            mockUserService.Setup(x => x.RegisterAsync(model));

            var controller = new RegisterController(mockUserService.Object);

            var result =await controller.Register(model);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirectResult.ActionName);
        }
    }
}
