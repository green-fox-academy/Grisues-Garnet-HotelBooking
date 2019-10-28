using System.Threading.Tasks;
using AutoMapper;
using HotelBookingGarnet;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Services;
using HotelBookingGarnetTest.TestUtils;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace HotelBookingGarnetTest.Services
{
    [Collection("Database collection")]
    public class ReservationServiceTest
    {
        private readonly DbContextOptions<ApplicationContext> options;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<IGuestService> mockGuestService;
        private readonly Mock<IRoomService> mockRoomService;
        private readonly Mock<IUserService> mockUserService;
        private readonly Mock<IHotelService> mockHotelService;

        public ReservationServiceTest()
        {
            options = TestDbOptions.Get();
            mockMapper = new Mock<IMapper>();
            mockGuestService = new Mock<IGuestService>();
            mockUserService = new Mock<IUserService>();
            mockHotelService = new Mock<IHotelService>();
            mockRoomService = new Mock<IRoomService>();
        }
        
        [Fact]
        public async Task CalculatePriceAsync_SimpleValuesShouldCalculate()
        {
            using (var context = new ApplicationContext(options))
            {
                var reservationService = new ReservationService(mockMapper.Object, context, mockGuestService.Object,
                    mockRoomService.Object, mockUserService.Object, mockHotelService.Object);
                var reservation = await reservationService.FindReservationByReservationIdAsync(1);

                mockRoomService.Setup(x => x.FindRoomByIdAsync(1))
                    .Returns(Task.FromResult(new Room
                    {
                        RoomId = 1,
                        Price = 400
                    }));
                    
                const int expected = 1200;
                var actual = await reservationService.CalculatePriceAsync(1, reservation);
                Assert.Equal(expected, actual);
            }
        }
        
        
    }
}