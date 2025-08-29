using FocusForge.Abstractions.Queries;
using FocusForge.TimeTracker.Entities;
using FocusForge.TimeTracker.TimeCamp.ApiCommunication;
using FocusForge.TimeTracker.TimeCamp.ApiCommunication.Client;
using FocusForge.TimeTracker.TimeCamp.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.Services.TimeTracking
{
    internal class TimeCampTasksLoader : ITasksLoader
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public TimeCampTasksLoader(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }
        public async Task<List<TimeTrackerTask>> LoadTasksAsync()
        {
            var dtos = await _queryDispatcher.QueryAsync(new AvailableTaskaQuery());
            return dtos.Select(d => new TimeTrackerTask
            {
                Name = d.name,
                ExternalId = d.task_id
            }).ToList();
        }
    }
}
