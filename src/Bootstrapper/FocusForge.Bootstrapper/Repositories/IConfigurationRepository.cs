using System;
using System.Threading.Tasks;
using Wachman.Models;

namespace Wachman.Repositories;

public interface IConfigurationRepository
{
    Task<PomodoroConfiguration> GetConfigurationAsync();
    Task SaveConfigurationAsync(PomodoroConfiguration configuration);
}
