using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wachman.Entities;
using Wachman.Models;

namespace Wachman.DAL
{
    internal static class SeedDataGenerator
    {
        public static AppSetting[] Generate()
        {
            var configuration = new PomodoroConfiguration();
            var settings = new[] {new AppSetting { Id = 1, SettingsKey = nameof(configuration.WorkSessionDuration), SettingsValue = configuration.WorkSessionDuration.ToString() },
            new AppSetting { Id = 2, SettingsKey = nameof(configuration.BreakTimeDuration), SettingsValue = configuration.BreakTimeDuration.ToString() },
            new AppSetting { Id = 3, SettingsKey = nameof(configuration.DisableBreaks), SettingsValue = configuration.DisableBreaks == true ? "1" : "0" } };
            return settings;
        }
    }
}
