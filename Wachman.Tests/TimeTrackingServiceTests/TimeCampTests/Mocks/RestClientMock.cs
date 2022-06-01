using RestSharp;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wachman.Tests.TimeTrackingServiceTests.Mocks
{
    internal class RestClientMock
    {
        private const string RestUrl = "https://app.timecamp.com/third_party/api";
        private readonly Dictionary<string, string> endpoints;

        public RestClientMock()
        {
            endpoints = new Dictionary<string, string>();
            endpoints.Add($"{RestUrl}/entires", MockResponses.EntriesResponse);
        }
        internal RestClient GetRestClient()
        {
            var mockHttp = new MockHttpMessageHandler();
            foreach (var key in endpoints.Keys)
            {
                mockHttp.When(key)
                    .Respond("application/json", endpoints[key]); // Respond with JSON
            }

            var options = new RestClientOptions(RestUrl)
            {
                ConfigureMessageHandler = handler => mockHttp
            };
            var client = new RestClient(options);

            return client;
        }


    }
}
