using FocusForge.TimeTracker.Repositories;
using FocusForge.TimeTracker.TimeCampAPI.DummyAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.Integrations.TimeCamp
{
    public class TimeCampApiFactory
    {
        private readonly ITimeTrackerSettingsRepository _settingsRepository;

        public TimeCampApiFactory(ITimeTrackerSettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        public ITimeTrackingService Create()
        {
            var apiKey = _settingsRepository.TimeCampApiKey;
            if (string.IsNullOrEmpty(apiKey))
            {
                return new DummyTrackingService();
            }
            return new TimeCampService(apiKey);
        }
    }
}
