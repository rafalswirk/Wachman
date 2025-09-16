using FocusForge.PomodoroTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.PomodoroTimer.Repositories
{
    public interface ITimeCampIntegrationRepository
    {
        TimeCampIntegrationData TimeCampApiKey { get; }
        void SaveTimeCampIntegrationData(TimeCampIntegrationData integrationData);
    }
}
