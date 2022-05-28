using DataModels.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTrackingService
{
    internal interface ITimeTrackingService
    {
        Task<bool> InitializeAsync();
        Task<List<Job>> GetDailyJobsAsync();
    }
}
