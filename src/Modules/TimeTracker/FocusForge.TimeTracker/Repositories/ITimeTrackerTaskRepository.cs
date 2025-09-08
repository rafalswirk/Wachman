using FocusForge.TimeTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.Repositories
{
    public interface ITimeTrackerTaskRepository
    {
        Task<TimeTrackerTask> CreateTaskAsync(TimeTrackerTask task);

        Task<TimeTrackerTask> GetTaskByIdAsync(int taskId);

        Task<TimeTrackerTask> UpdateTaskAsync(TimeTrackerTask task);

        Task<bool> DeleteTaskAsync(int taskId);

        Task<IEnumerable<TimeTrackerTask>> GetAllTasksAsync();
    }
}
