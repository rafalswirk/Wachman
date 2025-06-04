using FocusForge.TimeTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.Repositories
{
    internal interface ITimeTrackerTaskRepository
    {
        Task<TimeTrackerTask> CreateTaskAsync(TimeTrackerTask task);

        Task<TimeTrackerTask> GetTaskByIdAsync(string taskId);

        Task<TimeTrackerTask> UpdateTaskAsync(TimeTrackerTask task);

        Task<bool> DeleteTaskAsync(string taskId);

        Task<IEnumerable<TimeTrackerTask>> GetAllTasksAsync();
    }
}
