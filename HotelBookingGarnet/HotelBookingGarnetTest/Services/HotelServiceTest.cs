using System.Collections.Generic;
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
    public class HotelServiceTest
    {
        private readonly DbContextOptions<ApplicationContext> options;
        private readonly Mock<IPropertyTypeService> mockPropertyTypeService;
        private readonly Mock<IImageService> mockImageService;
        private readonly Mock<IMapper> mockMapper;
        
        public HotelServiceTest()
        {
            options = TestDbOptions.Get();
            mockPropertyTypeService = new Mock<IPropertyTypeService>();
            mockImageService = new Mock<IImageService>();
            mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task AverageRating_ShouldCalculateAverage()
        {
            using (var context = new ApplicationContext(options))
            {
                var hotelService = new HotelService(context, mockPropertyTypeService.Object, mockImageService.Object, 
                    mockMapper.Object);

                List<Review> reviews = new List<Review>();
                reviews.Add(new Review{Rating = 4});
                reviews.Add(new Review{Rating = 2});
                reviews.Add(new Review{Rating = 3});
                
                var actual = hotelService.AverageRating(reviews);
                var expected = 3;
                Assert.Equal(expected, actual);
            }
        }
    }
}