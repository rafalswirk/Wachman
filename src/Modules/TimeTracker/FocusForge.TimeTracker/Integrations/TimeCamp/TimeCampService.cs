using FocusForge.DataModels.Jobs;
using FocusForge.TimeTracker.Services.TimeTracking;
using FocusForge.TimeTracker.TimeCamp.ApiCommunication;
using FocusForge.TimeTracker.TimeCamp.ApiCommunication.Client;
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

        public TimeCampService(ITimeCampApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<string> GetCurrentJobName()
        {
            var statusReader = new TimeCampStatusReader(_apiClient);
            return await statusReader.GetCurrentJobAsync();
        }

        public async Task<List<Job>?> GetDailyJobsAsync()
        {
            var dailyJobsReader = new GetDailyJobs(_apiClient);
            return await dailyJobsReader.ExecuteAsync();
        }

        public Task<bool> InitializeAsync()
        {
            _apiClient.Initialize(_key);
            return Task.FromResult(true);
        }

        public async Task StartNewJobAsync()
        {
            var startJobCommand = new TimeCampStartJobCommand(_apiClient);
            await startJobCommand.ExecuteAsync();
        }

        public async Task<bool> IsJobRunningAsync()
        {
            var statusReader = new IsTimeCampJobRunning(_apiClient);
            return await statusReader.ExecuteAsync();
        }

        public Task StopCurrentJob()
        {
            throw new NotImplementedException();
        }
    }
}
