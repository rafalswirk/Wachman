using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.TimeCamp.ApiCommunication
{
    internal class TimeCampStartJobCommand
    {
        private readonly RestClient _apiClient;

        public TimeCampStartJobCommand(RestClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task ExecuteAsync()
        {
            var request = new RestRequest
            {
                Resource = $"/timer",
                Method = Method.Post
            };
            var requestBody = new
            {
                action = "start"
            };

            request.AddBody(requestBody);
            var response = await _apiClient.ExecuteAsync(request);
        }
    }
}
