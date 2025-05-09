using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.Repositories
{
    internal interface ITimeTrackerSettingsRepository
    {
        string TimeCampApiKey { get; }
        void SaveTimeCampApiKey(string timeCampApiKey);
    }
}
