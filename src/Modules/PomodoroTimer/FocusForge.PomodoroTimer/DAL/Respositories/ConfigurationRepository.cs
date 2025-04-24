using System;
using System.Linq;
using System.Threading.Tasks;
using Wachman.Entities;
using Wachman.Models;
using Wachman.Repositories;

namespace Wachman.DAL.Respositories;

public class ConfigurationRepository : IConfigurationRepository
{
    private readonly WachmanDbContext _context;

    public ConfigurationRepository(WachmanDbContext context)
    {
        _context = context;
    }

    public Task<PomodoroConfiguration> GetConfigurationAsync()
    {
        var configuration = new PomodoroConfiguration();

        configuration.WorkSessionDuration = int.Parse(GetValue(nameof(configuration.WorkSessionDuration)));
        configuration.BreakTimeDuration = int.Parse(GetValue(nameof(configuration.BreakTimeDuration)));
        configuration.DisableBreaks = GetValue(nameof(configuration.DisableBreaks)) == "1";
        
        return Task.FromResult(configuration);
    }

    public async Task SaveConfigurationAsync(PomodoroConfiguration configuration)
    {
        var workSessionDurationSetting = _context.Settings.Single(s => s.SettingsKey == nameof(configuration.WorkSessionDuration));
        workSessionDurationSetting.SettingsValue = configuration.WorkSessionDuration.ToString();

        var breakTimeDurationSetting = _context.Settings.Single(s => s.SettingsKey == nameof(configuration.BreakTimeDuration));
        breakTimeDurationSetting.SettingsValue = configuration.BreakTimeDuration.ToString();

        var disableBreaksSetting = _context.Settings.Single(s => s.SettingsKey == nameof(configuration.DisableBreaks));
        disableBreaksSetting.SettingsValue = configuration.DisableBreaks ? "1" : "0";

        await _context.SaveChangesAsync();
    }

    private string GetValue(string key)
        => _context.Settings.Single(s => s.SettingsKey == key).SettingsValue;
}
