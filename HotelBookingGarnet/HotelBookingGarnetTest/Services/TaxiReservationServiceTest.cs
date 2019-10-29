using AutoMapper;
using HotelBookingGarnet;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
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
    public class TaxiReservationServiceTest
    {
        private readonly DbContextOptions<ApplicationContext> options;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<IUserService> mockUserService;

        public TaxiReservationServiceTest()
        {
            this.options = TestDbOptions.Get();
            this.mockMapper = new Mock<IMapper>();
            this.mockUserService =new Mock<IUserService>();
        }

        [Fact]
        public async Task TaxiReservationValidationAsyncTest()
        {
            using (var context = new ApplicationContext(options))
            {
                var taxiReservationService = new TaxiReservationService(context, mockMapper.Object, mockUserService.Object);
                var taxiReservation = new TaxiReservationViewModel { StartLocal = "Budapest", EndLocal = "Siófok", TaxiReservationStart = new DateTime(2019, 10, 11), NumberOfGuest = 2, PhoneNumber = "05202222" };

                List<string> expected = new List<string>() { "The booking cannot begin earlier than today!" };
                var actual = await taxiReservationService.TaxiReservationValidationAsync(taxiReservation);
                Assert.Equal(expected, actual);
            }
        }
    }
}