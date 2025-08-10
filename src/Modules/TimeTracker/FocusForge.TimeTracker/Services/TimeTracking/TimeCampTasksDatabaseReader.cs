using FocusForge.TimeTracker.DAL;
using FocusForge.TimeTracker.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.Services.TimeTracking
{
    public class TimeCampTasksDatabaseReader : ITasksReader
    {
        private readonly TimeTrackerDbContext _context;

        public TimeCampTasksDatabaseReader(TimeTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<TimeTrackerTask>> ReadAsync()
            => await _context.TimeTrackerTasks.ToListAsync();
    }
}
