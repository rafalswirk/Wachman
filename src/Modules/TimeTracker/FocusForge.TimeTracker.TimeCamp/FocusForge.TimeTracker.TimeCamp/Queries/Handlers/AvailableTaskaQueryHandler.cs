using FocusForge.Shared.Abstractions.Queries;
using FocusForge.TimeTracker.TimeCamp.ApiCommunication.Client;
using FocusForge.TimeTracker.TimeCamp.DAL.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.TimeCamp.Queries.Handlers
{
    public class AvailableTaskaQueryHandler : IQueryHandler<AvailableTaskaQuery, IEnumerable<TaskInfoDTO>>
    {
        private readonly ITimeCampApiClient _apiClient;

        public AvailableTaskaQueryHandler(ITimeCampApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<TaskInfoDTO>> HandleAsync(AvailableTaskaQuery query)
        {
            var result = new List<TaskInfoDTO>();

            var request = new RestRequest
            {
                Resource = $"/tasks",
                Method = Method.Get
            };

            request.AddParameter("exclude_archived", 1);
            request.AddParameter("minimal", 1);

            var response = await _apiClient.Client.ExecuteAsync<Dictionary<string, TaskInfoDTO>>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                result = response.Data.Select(x => new TaskInfoDTO(x.Value.task_id, x.Value.name)).ToList();
            }
            else
            {
                throw new Exception($"Failed to retrieve tasks: {response.ErrorMessage}");
            }

            return result;
        }
    }
}
