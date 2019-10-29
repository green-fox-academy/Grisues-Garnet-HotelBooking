using System.Threading.Tasks;
using HotelBookingGarnet;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace HotelBookingGarnetTest
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public IntegrationTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task MainPageLoadSuccessfully()
        {
            var responseMessage = await factory.CreateClient().GetAsync("/");
            responseMessage.EnsureSuccessStatusCode();
        }
    }
}