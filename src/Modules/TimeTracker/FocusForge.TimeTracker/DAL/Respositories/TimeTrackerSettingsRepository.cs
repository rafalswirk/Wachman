using FocusForge.Shared.Abstractions.Events;
using FocusForge.Shared.DataModels.Entities;
using FocusForge.TimeTracker.Messages.Events;
using FocusForge.TimeTracker.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.DAL.Respositories
{
    internal class TimeTrackerSettingsRepository : ITimeTrackerSettingsRepository
    {
        private readonly TimeTrackerDbContext _timeTrackerDbContext;
        private readonly IEventDispatcher _eventDispatcher;

        public TimeTrackerSettingsRepository(TimeTrackerDbContext timeTrackerDbContext, IEventDispatcher eventDispatcher)
        {
            _timeTrackerDbContext = timeTrackerDbContext;
            _eventDispatcher = eventDispatcher;
        }

        public string TimeCampApiKey
            => _timeTrackerDbContext.Settings.SingleOrDefault(s => s.SettingsKey == nameof(TimeCampApiKey))?.SettingsValue ?? string.Empty;

        public async Task SaveTimeCampApiKey(string timeCampApiKey)
        {
            var apiKey = _timeTrackerDbContext.Settings.SingleOrDefault(s => s.SettingsKey == nameof(TimeCampApiKey));
            if(apiKey == null) 
                _timeTrackerDbContext.Settings.Add(new AppSetting { SettingsKey = nameof(TimeCampApiKey), SettingsValue = timeCampApiKey });
            else
                apiKey.SettingsValue = timeCampApiKey;

            _timeTrackerDbContext.SaveChanges();
            await _eventDispatcher.PublishAsync(new TimeCampApiKeyProvided(timeCampApiKey));
        }
    }
}
