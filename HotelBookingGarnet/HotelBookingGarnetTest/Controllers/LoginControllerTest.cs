using HotelBookingGarnet.Services;
using Moq;

namespace HotelBookingGarnetTest.Controllers
{
    public class LoginControllerTest
    {
        private readonly Mock<IUserService> userService;

        public LoginControllerTest()
        {
            userService = new Mock<IUserService>();
        }
    }
}