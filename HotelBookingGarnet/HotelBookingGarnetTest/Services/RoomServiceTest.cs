using System.Threading.Tasks;
using AutoMapper;
using HotelBookingGarnet;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using HotelBookingGarnetTest.TestUtils;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace HotelBookingGarnetTest.Services
{
    [Collection("Database collection")]
    public class RoomServiceTest
    {
        private readonly DbContextOptions<ApplicationContext> options;
        private readonly Mock<IHotelService> mockHotelService;
        private readonly Mock<IMapper> mockMapper;

        public RoomServiceTest()
        {
            options = TestDbOptions.Get();
            mockHotelService = new Mock<IHotelService>();
            mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task AddRoomAsync_ShouldAddNewRoom()
        {
            using (var context = new ApplicationContext(options))
            {
                var roomModel = new RoomViewModel()
                {
                    RoomName = "NewRoom",
                    Price = 100,
                    NumberOfGuests = 1,
                    NumberOfRooms = 1,
                };
                
                mockMapper.Setup(x => x.Map<RoomViewModel, Room>(It.IsAny<RoomViewModel>()))
                    .Returns(new Room
                    {
                        RoomName = roomModel.RoomName,
                        Price = roomModel.Price,
                        NumberOfGuests = roomModel.NumberOfGuests,
                        NumberOfRooms = roomModel.NumberOfRooms
                    });
                var hotel = await context.Hotels.FindAsync(1L);
                mockHotelService.Setup(x => x.FindHotelByIdAsync(1))
                    .Returns(Task.FromResult(hotel));

                var roomService = new RoomService(context, mockHotelService.Object, mockMapper.Object);
                var lenght = await context.Rooms.CountAsync();
                await roomService.AddRoomAsync(roomModel, 1);
                Assert.Equal(lenght + 1, await context.Rooms.CountAsync());
            }
        }
    }
}