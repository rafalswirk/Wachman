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
                Resource = $"/timer_running",
                Method = Method.Get
            };
            var response = await _apiClient.Client.GetAsync<List<TimerStatusDTO>>(request);

            if (response?.Count > 0)
                return true;

            return false;
        }
    }
}
