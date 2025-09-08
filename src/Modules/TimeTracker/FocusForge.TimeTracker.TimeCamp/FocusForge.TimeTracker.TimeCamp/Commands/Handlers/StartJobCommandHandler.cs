using FocusForge.Shared.Abstractions.Commands;
using FocusForge.TimeTracker.TimeCamp.ApiCommunication.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.TimeCamp.Commands.Handlers
{
    public class StartJobCommandHandler : ICommandHandler<StartJobCommand>
    {
        private readonly ITimeCampApiClient _apiClient;

        public StartJobCommandHandler(ITimeCampApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task HandleAsync(StartJobCommand command)
        {
            var request = new RestRequest
            {
                Resource = $"/timer",
                Method = Method.Post
            };
            var requestBody = new
            {
                action = "start",
                task_id = command.TaskId
            };

            request.AddBody(requestBody);
            var response = await _apiClient.Client.ExecuteAsync(request);
        }
    }
}
