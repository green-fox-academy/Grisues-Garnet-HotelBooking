using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    [Collection("DatabaseCollection")]
    public class HotelServiceTest
    {
        private readonly DbContextOptions<ApplicationContext> options;
        private readonly Mock<IPropertyTypeService> mockPropertyTypeService;
        private readonly Mock<IImageService> mockImageService;
        private readonly Mock<IMapper> mockMapper;

        public HotelServiceTest()
        {
            options = TestDbOptions.Get();
            mockMapper = new Mock<IMapper>();
            mockImageService = new Mock<IImageService>();
            mockPropertyTypeService = new Mock<IPropertyTypeService>();
        }

        [Fact]
        public async Task Add_Hotel_Should_Increase_Number_Of_Hotels()
        {
            using (var context = new ApplicationContext(options))
            {
                var hotel = new HotelViewModel
                {
                    HotelName = "Test",
                    Country = "Test", 
                    Region = "test", 
                    Address = "test", 
                    City = "test", 
                    Description = "test",
                };
                
                var hotelService = new HotelService(context, mockPropertyTypeService.Object, 
                    mockImageService.Object,mockMapper.Object );

                mockPropertyTypeService.Setup(x => x.AddPropertyTypeAsync(It.IsAny<string>()))
                    .Returns(Task.FromResult(new PropertyType()
                    {
                        PropertyTypeId = 1,
                    }));

                mockMapper.Setup(x => x.Map<HotelViewModel, Hotel>(It.IsAny<HotelViewModel>()))
                    .Returns(new Hotel()
                    {
                        HotelName = hotel.HotelName,
                        Country = hotel.Country,
                        Region = hotel.Region,
                        Address = hotel.Address,
                        City = hotel.City,
                        Description = hotel.Description

                    });
                var length = await context.Hotels.CountAsync();
                var actual = await hotelService.AddHotelAsync(hotel, "123");
                Assert.Equal(length +1 , await context.Hotels.CountAsync());
            }
        }
    }
}