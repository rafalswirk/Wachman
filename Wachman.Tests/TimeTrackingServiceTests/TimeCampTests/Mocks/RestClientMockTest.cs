using FluentAssertions;
using RestSharp;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Wachman.Tests.TimeTrackingServiceTests.Mocks
{
    public class RestClientMockTest
    {
        [Fact]
        public async Task GetAsync_FromTestRequest_ReturnsTestReposne()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://app.timecamp.com/third_party/api/test")
                    .Respond("application/json", "{'name' : 'Test McGee'}"); // Respond with JSON
            var options = new RestClientOptions(@"https://app.timecamp.com/third_party/api")
            {
                ConfigureMessageHandler = handler => mockHttp
            };
            var client = new RestClient(options);
            var request = new RestRequest("/test");

            var response = await client.GetAsync(request);

            response.Content.Should().Be("{'name' : 'Test McGee'}");
        }
    }
}
