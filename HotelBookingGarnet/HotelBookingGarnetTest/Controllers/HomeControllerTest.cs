using HotelBookingGarnet.Controllers.Home;
using HotelBookingGarnet.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace HotelBookingGarnetTest.Controllers
{
    public class HomeControllerTest
    {
        private readonly Mock<IHotelService> mockHotelService;
        private readonly Mock<IUserService> mockUserService;

        public HomeControllerTest()
        {
            mockHotelService = new Mock<IHotelService>();
            mockUserService = new Mock<IUserService>();        }

        [Fact]
        public async Task Logout_UserShouldLogout()
        {
            var controller = new HomeController(mockHotelService.Object, mockUserService.Object);

            var result =await controller.Logout();
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirectResult.ActionName);
        }
    }
}
