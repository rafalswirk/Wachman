using FocusForge.Abstractions.Queries;
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
        private readonly IQueryDispatcher _queryDispatcher;

        public TimeCampApiFactory(ITimeTrackerSettingsRepository settingsRepository, ITimeCampApiClient apiClient, IQueryDispatcher queryDispatcher)
        {
            _settingsRepository = settingsRepository;
            _apiClient = apiClient;
            _queryDispatcher = queryDispatcher;
        }

        public ITimeTrackingService Create()
        {
            var apiKey = _settingsRepository.TimeCampApiKey;
            if (string.IsNullOrEmpty(apiKey))
            {
                return new DummyTrackingService();
            }
            return new TimeCampService(_apiClient, _queryDispatcher);
        }
    }
}
