using AutoMapper;
using HotelBookingGarnet;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using HotelBookingGarnetTest.TestUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration configuration;

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
                var taxiReservationService = new TaxiReservationService(context, mockMapper.Object, mockUserService.Object, configuration );
                var taxiReservation = new TaxiReservationViewModel { StartLocal = "Budapest", EndLocal = "Siófok", TaxiReservationStart = new DateTime(2019, 10, 11), NumberOfGuest = 2, PhoneNumber = "05202222" };

                List<string> expected = new List<string>() { "The booking cannot begin earlier than today!" };
                var actual = taxiReservationService.TaxiReservationValidation(taxiReservation);
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public async Task FindTaxiReservationByTaxiReservationIdAsyncTest()
        {
            using (var context = new ApplicationContext(options))
            {
                var taxiReservationService = new TaxiReservationService(context, mockMapper.Object, mockUserService.Object, configuration);
                var taxiReservation = new TaxiReservation
                {
                    TaxiReservationStart = new DateTime(2019, 11, 10),
                    TaxiReservationId = 1,
                    NumberOfGuest = 2,
                    PhoneNumber = "222222222",
                    StartLocal = "Budapest",
                    EndLocal = "Siófok",
                    UserId = "1"
                };
                long id = taxiReservation.TaxiReservationId;
                var actual = await taxiReservationService.FindTaxiReservationByIdAsync(1);
                Assert.Equal(id, actual.TaxiReservationId);
            }
        }
        [Fact]
        public async Task FindTaxiReservationByUserIdAsyncTest()
        {
            using (var context = new ApplicationContext(options))
            {
                var taxiReservationService = new TaxiReservationService(context, mockMapper.Object, mockUserService.Object, configuration);
                var taxiReservation = new TaxiReservation
                {
                    TaxiReservationStart = new DateTime(2019, 11, 10),
                    TaxiReservationId = 1,
                    NumberOfGuest = 2,
                    PhoneNumber = "222222222",
                    StartLocal = "Budapest",
                    EndLocal = "Siófok",
                    UserId = "1"
                };
                //new TaxiReservation expected = new TaxiReservation;
                string id = taxiReservation.UserId;
                var actual = await taxiReservationService.FindTaxiReservationByIdAsync(1);
                Assert.Equal(id, actual.UserId);
            }
        }
    }
}