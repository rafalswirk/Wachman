using FocusForge.TimeTracker.DAL;
using FocusForge.TimeTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.Services.TimeTracking
{
    internal class TimeCampTasksDatabaseWriter : ITasksWriter
    {
        private readonly TimeTrackerDbContext _context;

        public TimeCampTasksDatabaseWriter(TimeTrackerDbContext context)
        {
            _context = context;
        }

        public async Task SaveTasksAsync(List<TimeTrackerTask> tasks)
        {
            _context.TimeTrackerTasks.RemoveRange(_context.TimeTrackerTasks);
            await _context.SaveChangesAsync();
            _context.AddRange(tasks);
            await _context.SaveChangesAsync();
        }
    }
}
