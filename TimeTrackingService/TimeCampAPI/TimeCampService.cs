using DataModels.Jobs;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTrackingService.TimeCampAPI.Client;

namespace TimeTrackingService.TimeCampAPI
{
    public class TimeCampService : ITimeTrackingService
    {
        private readonly string _key;

        public TimeCampService(string key)
        {
            _key = key;
        }

        public async Task<string> GetCurrentJobName()
        {
            var statusReader = new TimeCampStatusReader(_key);
            return await statusReader.GetCurrentJobAsync();
        }

        public async Task<List<Job>?> GetDailyJobsAsync()
        {
            var dailyJobsReader = new GetDailyJobs(TimeCampApiClient.Instance);
            return await dailyJobsReader.ExecuteAsync();
        }

        public Task<bool> InitializeAsync()
        {
            TimeCampApiClient.Initialize(_key);
            return Task.FromResult(true);
        }
    }
}
