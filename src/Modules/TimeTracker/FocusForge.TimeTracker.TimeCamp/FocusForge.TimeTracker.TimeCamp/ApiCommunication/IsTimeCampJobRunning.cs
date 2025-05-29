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
        private readonly RestClient _apiClient;

        public IsTimeCampJobRunning(RestClient apiClient)
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
            var response = await _apiClient.ExecuteAsync<TimerStatusDTO>(request);
            return response.Data?.IsTimerRunning ?? false;

        }
    }
}
