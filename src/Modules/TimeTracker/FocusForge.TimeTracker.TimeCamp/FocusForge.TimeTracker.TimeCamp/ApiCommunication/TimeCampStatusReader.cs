using FocusForge.Abstractions.Queries;
using FocusForge.TimeTracker.TimeCamp.ApiCommunication.Client;
using FocusForge.TimeTracker.TimeCamp.DAL.DTO;
using FocusForge.TimeTracker.TimeCamp.Queries;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FocusForge.TimeTracker.TimeCamp.ApiCommunication
{
    public class TimeCampStatusReader : IQueryHandler<TimerStatusQuery, TimerStatusDTO>
    {
        private readonly ITimeCampApiClient _apiClient;

        public TimeCampStatusReader(ITimeCampApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<TimerStatusDTO> HandleAsync(TimerStatusQuery query)
        {
            try
            {
                var request = new RestRequest
                {
                    Resource = $"/timer_running",
                    Method = Method.Get
                };
                var response = await _apiClient.Client.GetAsync<List<TimerStatusDTO>>(request);

                if (response?.Count == 0)
                    return CreateEmpty();
                return response?.First() ?? CreateEmpty();
            }
            catch (Exception)
            {
                return CreateEmpty();
            }
        }

        private TimerStatusDTO CreateEmpty()
            => new TimerStatusDTO("");
    }
}
