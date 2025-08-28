using FocusForge.TimeTracker.Entities;
using FocusForge.TimeTracker.TimeCamp.ApiCommunication;
using FocusForge.TimeTracker.TimeCamp.ApiCommunication.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.Services.TimeTracking
{
    internal class TimeCampTasksLoader : ITasksLoader
    {
        private readonly ITimeCampApiClient _apiClient;

        public TimeCampTasksLoader(ITimeCampApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<List<TimeTrackerTask>> LoadTasksAsync()
        {
            var reader = new GetAvailableTasks(_apiClient);
            var dtos = await reader.GetTasksAsync();
            return dtos.Select(d => new TimeTrackerTask
            {
                Name = d.name,
                ExternalId = d.task_id
            }).ToList();
        }
    }
}
