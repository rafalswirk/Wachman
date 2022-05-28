using DataModels.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTrackingService.TimeCampAPI
{
    public class TimeCampService : ITimeTrackingService
    {
        public Task<List<Job>> GetDailyJobsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> InitializeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
