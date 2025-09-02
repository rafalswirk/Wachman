using FocusForge.Abstractions.Queries;
using FocusForge.DataModels.Jobs;
using FocusForge.TimeTracker.Services.TimeTracking;
using FocusForge.TimeTracker.TimeCamp.ApiCommunication;
using FocusForge.TimeTracker.TimeCamp.ApiCommunication.Client;
using FocusForge.TimeTracker.TimeCamp.Queries;
using FocusForge.TimeTracker.TimeCamp.Queries.Handlers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.Integrations.TimeCamp
{
    public class TimeCampService : ITimeTrackingService
    {
        private readonly string _key;
        private ITimeCampApiClient _apiClient;
        private readonly IQueryDispatcher _queryDispatcher;

        public TimeCampService(ITimeCampApiClient apiClient, IQueryDispatcher queryDispatcher)
        {
            _apiClient = apiClient;
            _queryDispatcher = queryDispatcher;
        }

        public async Task<string> GetCurrentJobName()
        {
            var result = await _queryDispatcher.QueryAsync(new TimerStatusQuery());
            return result.Name;
        }

        public async Task<List<Job>> GetDailyJobsAsync()
        {
            var result = await _queryDispatcher.QueryAsync(new DailyJobsQuery());
            return result.Jobs;
        }

        public Task<bool> InitializeAsync()
        {
            _apiClient.Initialize(_key);
            return Task.FromResult(true);
        }

        public async Task StartNewJobAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsJobRunningAsync()
        {
            var result = await _queryDispatcher.QueryAsync(new JobTimerRunningQuery());
            return result.IsRunning;
        }

        public Task StopCurrentJob()
        {
            throw new NotImplementedException();
        }
    }
}
