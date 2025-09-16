using FocusForge.PomodoroTimer.Models;
using FocusForge.PomodoroTimer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.PomodoroTimer.DAL.Respositories
{
    internal class TimeCampIntegrationRepository : ITimeCampIntegrationRepository
    {
        private readonly WachmanDbContext _context;

        public TimeCampIntegrationRepository(WachmanDbContext context)
        {
            _context = context;
        }
        public TimeCampIntegrationData TimeCampApiKey
        {
            get
            {
                var integrationData = new TimeCampIntegrationData();
                var setting = _context.Settings.SingleOrDefault(s => s.SettingsKey == nameof(integrationData.ApiKey));
                integrationData.ApiKey = setting?.SettingsValue ?? string.Empty;
                return integrationData;
            }
        }

        public void SaveTimeCampIntegrationData(TimeCampIntegrationData timeCampApiKey)
        {
            var data = _context.Settings.SingleOrDefault(s => s.SettingsKey == nameof(timeCampApiKey.ApiKey));
            if(data == null)
                _context.Settings.Add(new Shared.DataModels.Entities.AppSetting { SettingsKey = nameof(timeCampApiKey.ApiKey), SettingsValue = timeCampApiKey.ApiKey });
            else
                data.SettingsValue = timeCampApiKey.ApiKey;
            _context.SaveChanges();
        }
    }
}
