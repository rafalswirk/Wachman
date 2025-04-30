
using FocusForge.PomodoroTimer.DAL;
using FocusForge.PomodoroTimer.DAL.Respositories;
using FocusForge.PomodoroTimer.DataStorage;
using FocusForge.PomodoroTimer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FocusForge.PomodoroTimer
{
    public static class Extensions
    {
        public static void AddPomodoroTimerCore(this IServiceCollection services)
        {
            services.AddSingleton<IApiKeyProvider, ApiKeyProvider>();
            services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
            services.AddScoped<WachmanDbContext>();
            
        }

        public static void ApplyMigrations(this IHost host)
        {
            var context = host.Services.GetRequiredService<WachmanDbContext>();
            context.Database.Migrate();
        }
    }

}
