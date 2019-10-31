using HotelBookingGarnet.Controllers.Home;
using HotelBookingGarnet.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotelBookingGarnetTest.Controllers
{
    public class HomeControllerTest
    {
        private readonly Mock<IHotelService> mockHotelService;
        private readonly Mock<IUserService> mockUserService;
        private readonly Mock<IImageService> mockImageService;

        public HomeControllerTest()
        {
            mockHotelService = new Mock<IHotelService>();
            mockUserService = new Mock<IUserService>();
            mockImageService = new Mock<IImageService>();
        }

        [Fact]
        public async Task Logout_UserShouldLogout()
        {
            mockUserService.Setup(x => x.Logout());

            var controller = new HomeController(mockHotelService.Object, mockUserService.Object, mockImageService.Object);

            var result =await controller.Logout();
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirectResult.ActionName);

        }
    }
}
