using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using HotelBookingGarnet.Controllers;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly Mock<IUserService> mockUserService;

        public ReservationControllerTest()
        {
            mockHotelService = new Mock<IHotelService>();
            mockGuestService = new Mock<IGuestService>();
            mockUserManager = new Mock<UserManager<User>>();
            mockReservationService = new Mock<IReservationService>();
            mockUserService = new Mock<IUserService>();
        }

        [Fact]
        public async Task AddReservation_ShouldAddOneReservation()
        {
            var reservationController = new ReservationController(mockUserManager.Object, mockReservationService.Object,
                mockHotelService.Object, mockGuestService.Object);
            
            var user = new User
            {
                Id = "1"
            };
            
            mockUserService.Setup(x => x.FindUserByHotelIdAsync(1))
                .Returns(Task.FromResult(user));
            
            var newReservation = new ReservationViewModel {NumberOfGuest = 2};
            var room = new Room {RoomId = 1};
            var hotel = new Hotel {HotelId = 2};
            
            var result = await reservationController.AddReservation(newReservation, room.RoomId, hotel.HotelId);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            mockReservationService.Verify(
                s => s.AddReservationAsync(newReservation, user.Id, room.RoomId, hotel.HotelId), Times.Once);
            Assert.Equal("AddReservation" , redirectResult.ActionName); 
        }
    }
}