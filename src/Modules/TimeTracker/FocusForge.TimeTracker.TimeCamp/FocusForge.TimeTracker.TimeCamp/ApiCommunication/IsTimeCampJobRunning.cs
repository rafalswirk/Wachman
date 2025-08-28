using FocusForge.TimeTracker.TimeCamp.ApiCommunication.Client;
using FocusForge.TimeTracker.TimeCamp.DAL.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.TimeCamp.ApiCommunication
{
    internal class IsTimeCampJobRunning
    {
        private readonly ITimeCampApiClient _apiClient;

        public IsTimeCampJobRunning(ITimeCampApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<bool> ExecuteAsync()
        {
            var request = new RestRequest
            {
                Resource = $"/timer",
                Method = Method.Post
            };
            var requestBody = new
            {
                action = "status"
            };

            request.AddBody(requestBody);
            var response = await _apiClient.Client.ExecuteAsync<TimerStatusDTO>(request);
            return response.Data?.IsTimerRunning ?? false;

        }
    }
}
