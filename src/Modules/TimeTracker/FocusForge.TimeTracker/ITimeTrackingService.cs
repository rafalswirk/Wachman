using FocusForge.DataModels.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker
{
    public interface ITimeTrackingService
    {
        Task<bool> InitializeAsync();
        Task<List<Job>?> GetDailyJobsAsync();
        Task<string> GetCurrentJobName();
        Task StartNewJob();
        Task StopCurrentJob();
    }
}
