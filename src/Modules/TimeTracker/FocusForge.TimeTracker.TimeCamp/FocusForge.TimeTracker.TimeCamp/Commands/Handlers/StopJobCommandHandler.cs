using FocusForge.Abstractions.Commands;
using FocusForge.TimeTracker.TimeCamp.ApiCommunication.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.TimeCamp.Commands.Handlers
{
    internal class StopJobCommandHandler : ICommandHandler<StopJobCommand>
    {
        private readonly ITimeCampApiClient _apiClient;

        public StopJobCommandHandler(ITimeCampApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task HandleAsync(StopJobCommand command)
        {
            var request = new RestRequest
            {
                Resource = $"/timer",
                Method = Method.Post
            };
            var requestBody = new
            {
                action = "stop",
            };

            request.AddBody(requestBody);
            var response = await _apiClient.Client.ExecuteAsync(request);
        }
    }
}