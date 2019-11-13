using HotelBookingGarnet.Controllers.Register;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotelBookingGarnetTest.Controllers
{
    public class RegisterControllerTest
    {
        private readonly Mock<IUserService> mockUserService;

        public RegisterControllerTest()
        {
            mockUserService = new Mock<IUserService>();
        }

        
    }
}
