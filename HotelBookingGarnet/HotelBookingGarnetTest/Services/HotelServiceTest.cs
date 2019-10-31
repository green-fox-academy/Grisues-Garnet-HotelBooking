using AutoMapper;
using AutoMapper.Configuration;
using HotelBookingGarnet;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Services;
using HotelBookingGarnetTest.TestUtils;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotelBookingGarnetTest.Services
{
    [Collection("Database collection")]
    public class HotelServiceTest
    {
        private readonly DbContextOptions<ApplicationContext> options;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<IImageService> mockImageService;
        private readonly Mock<IPropertyTypeService> mockPropertyTypeService;
        private readonly Mock<IBlobService> mockBlobService;

        public HotelServiceTest()
        {
            this.options = TestDbOptions.Get();
            this.mockMapper = new Mock<IMapper>();
            this.mockImageService = new Mock<IImageService>();
            this.mockPropertyTypeService = new Mock<IPropertyTypeService>();
            this.mockBlobService = new Mock<IBlobService>();
        }

        [Fact]
        public void GetHotels_ShouldGetAllHotelFromDatabase()
        {
            using (var context = new ApplicationContext(options))
            {
                var hotelService = new HotelService(context, mockPropertyTypeService.Object, mockImageService.Object, mockMapper.Object);

                var expected = hotelService.GetHotels();
                var actual = 1;
                Assert.Equal(expected.Count, actual);
            }
        }
    }
}