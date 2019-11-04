using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingGarnet.Controllers.Login;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Xunit.Sdk;

namespace HotelBookingGarnetTest.Controllers
{
    public class LoginControllerTest
    {
        private readonly Mock<IUserService> mockUserService;

        public LoginControllerTest()
        {
            mockUserService = new Mock<IUserService>();
        }

        [Fact]
        public async Task Successful_Login_Attempt_Should_Redirect_To_Main_Page()
        {
            var user = new User()
            {
                Id = "test",
                Email = "test@email.com"
            };
            var loginViewModel = new LoginViewModel()
            {
                Email = user.Email,
                Password = "test",
                ErrorMessages = new List<string>()
            };
            var controller = new LoginController(mockUserService.Object);
            mockUserService.Setup(x => x.LoginAsync(loginViewModel)).Returns(Task.FromResult(new List<string>()));
            var result = await controller.Login(loginViewModel);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            mockUserService.Verify(x => x.LoginAsync(loginViewModel), Times.Once);
            Assert.Equal("Index", redirectResult.ActionName);
        }
    }
}