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

        public TimeTrackerSettingsRepository(TimeTrackerDbContext timeTrackerDbContext)
        {
            _timeTrackerDbContext = timeTrackerDbContext;
        }

        public string TimeCampApiKey
            => _timeTrackerDbContext.Settings.SingleOrDefault(s => s.SettingsKey == nameof(TimeCampApiKey))?.SettingsValue ?? string.Empty;

        public void SaveTimeCampApiKey(string timeCampApiKey)
        {
            var apiKey = _timeTrackerDbContext.Settings.SingleOrDefault(s => s.SettingsKey == nameof(TimeCampApiKey));
            if(apiKey == null) 
                _timeTrackerDbContext.Settings.Add(new DataModels.Entities.AppSetting { SettingsKey = nameof(TimeCampApiKey), SettingsValue = timeCampApiKey });
            else
                apiKey.SettingsValue = timeCampApiKey;

            _timeTrackerDbContext.SaveChanges();
        }
    }
}
