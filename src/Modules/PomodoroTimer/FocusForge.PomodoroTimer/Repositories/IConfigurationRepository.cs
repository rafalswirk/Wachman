using FocusForge.PomodoroTimer.Models;
using System;
using System.Threading.Tasks;

namespace FocusForge.PomodoroTimer.Repositories;

public interface IConfigurationRepository
{
    Task<PomodoroConfiguration> GetConfigurationAsync();
    Task SaveConfigurationAsync(PomodoroConfiguration configuration);
}
