using System.Security.Claims;
using System.Threading.Tasks;
using HotelBookingGarnet.Controllers;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace HotelBookingGarnetTest.Controllers
{
    public class ReservationControllerTest
    {
        private readonly Mock<IHotelService> mockHotelService;
        private readonly Mock<IGuestService> mockGuestService;
        private readonly Mock<UserManager<User>> mockUserManager;
        private readonly Mock<IReservationService> mockReservationService;

        public ReservationControllerTest()
        {
            mockHotelService = new Mock<IHotelService>();
            mockGuestService = new Mock<IGuestService>();
            mockUserManager = new Mock<UserManager<User>>();
            mockReservationService = new Mock<IReservationService>();
        }

        [Fact]
        public async Task CancelReservation_ShouldDeleteOneReservation()
        {
            var user = new User
            {
                Id = "1"
            };

            new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            });
            mockUserManager.Setup(x => x.GetUserAsync(ClaimsPrincipal.Current))
                .Returns(Task.FromResult(user));

            var reservationController = new ReservationController(mockUserManager.Object, mockReservationService.Object,
                mockHotelService.Object, mockGuestService.Object);
            
            var newReservation = new ReservationViewModel {NumberOfGuest = 2};
            var room = new Room {RoomId = 1};
            var hotel = new Hotel {HotelId = 2};

            mockReservationService
                .Setup(x => x.AddReservationAsync(newReservation, user.Id, room.RoomId, hotel.HotelId))
                .Returns(Task.FromResult(new Reservation()
                {
                }));
            
            await reservationController.AddReservation()
        }
    }
}