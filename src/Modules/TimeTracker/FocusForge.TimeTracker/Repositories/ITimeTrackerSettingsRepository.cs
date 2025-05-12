using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.Repositories
{
    public interface ITimeTrackerSettingsRepository
    {
        string TimeCampApiKey { get; }
        void SaveTimeCampApiKey(string timeCampApiKey);
    }
}
