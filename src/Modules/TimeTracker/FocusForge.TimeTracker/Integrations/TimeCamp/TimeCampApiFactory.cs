using FocusForge.TimeTracker.Repositories;
using FocusForge.TimeTracker.Services.TimeTracking;
using FocusForge.TimeTracker.TimeCamp.ApiCommunication.Client;
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
        private readonly ITimeCampApiClient _apiClient;

        public TimeCampApiFactory(ITimeTrackerSettingsRepository settingsRepository, ITimeCampApiClient apiClient)
        {
            _settingsRepository = settingsRepository;
            _apiClient = apiClient;
        }

        public ITimeTrackingService Create()
        {
            var apiKey = _settingsRepository.TimeCampApiKey;
            if (string.IsNullOrEmpty(apiKey))
            {
                return new DummyTrackingService();
            }
            return new TimeCampService(_apiClient);
        }
    }
}
