using FocusForge.TimeTracker.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FocusForge.TimeTracker.Entities;

namespace FocusForge.TimeTracker.DAL.Respositories
{
    public class TimeTrackerTaskRepository : ITimeTrackerTaskRepository
    {
        private readonly TimeTrackerDbContext _context;

        public TimeTrackerTaskRepository(TimeTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<TimeTrackerTask> CreateTaskAsync(TimeTrackerTask task)
        {
            _context.TimeTrackerTasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TimeTrackerTask> GetTaskByIdAsync(int taskId)
        {
            return await _context.TimeTrackerTasks
                .FirstOrDefaultAsync(t => t.Id == taskId);
        }

        public async Task<IEnumerable<TimeTrackerTask>> GetAllTasksAsync()
        {
            return await _context.TimeTrackerTasks.ToListAsync();
        }

        public async Task<TimeTrackerTask> UpdateTaskAsync(TimeTrackerTask task)
        {
            _context.TimeTrackerTasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteTaskAsync(int  taskId)
        {
            var task = await _context.TimeTrackerTasks.FirstOrDefaultAsync(t => t.Id == taskId);
            if (task == null)
                return false;

            _context.TimeTrackerTasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
