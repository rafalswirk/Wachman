using FocusForge.DataModels.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.Services.TimeTracking
{
    public interface ITimeTrackingService
    {
        Task<bool> InitializeAsync();
        Task<List<Job>?> GetDailyJobsAsync();
        Task<string> GetCurrentJobName();
        Task StartNewJobAsync();
        Task StopCurrentJob();
        Task<bool> IsJobRunningAsync();
    }
}
