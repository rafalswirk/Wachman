using FocusForge.TimeTracker.TimeCamp.ApiCommunication.Client;
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
        private readonly ITimeCampApiClient _apiClient;

        public TimeCampStartJobCommand(ITimeCampApiClient apiClient)
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
            var response = await _apiClient.Client.ExecuteAsync(request);
        }
    }
}
