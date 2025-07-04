using FocusForge.TimeTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.Services.TimeTracking
{
    public interface ITasksWriter
    {
        Task SaveTasksAsync(List<TimeTrackerTask> tasks);
    }
}
