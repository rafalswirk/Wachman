using FocusForge.TimeTracker.Entities;
using FocusForge.TimeTracker.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.Integrations.TimeCamp
{
    internal class TimeCampTasksImporter
    {
        private readonly ITimeTrackerTaskRepository _timeTrackerTaskRepository;

        public TimeCampTasksImporter(ITimeTrackerTaskRepository timeTrackerTaskRepository)
        {
            _timeTrackerTaskRepository = timeTrackerTaskRepository;
        }

        public async Task<IReadOnlyCollection<TimeTrackerTask>> ImportAsync()
        {
            throw new NotImplementedException();
        }
    }
}
