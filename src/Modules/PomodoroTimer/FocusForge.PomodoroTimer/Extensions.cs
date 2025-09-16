
using FocusForge.PomodoroTimer.DAL;
using FocusForge.PomodoroTimer.DAL.Respositories;
using FocusForge.PomodoroTimer.Events.External.Handlers;
using FocusForge.PomodoroTimer.Repositories;
using FocusForge.Shared.Abstractions.Events;
using FocusForge.TimeTracker.Messages.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FocusForge.PomodoroTimer
{
    public static class Extensions
    {
        public static void AddPomodoroTimerCore(this IServiceCollection services)
        {
            services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
            services.AddScoped<ITimeCampIntegrationRepository, TimeCampIntegrationRepository>();
            services.AddScoped<WachmanDbContext>();
            services.AddScoped<IEventHandler<TimeCampApiKeyProvided>, TimeCampApiKeyProvidedHandler>();
        }

        public static void ApplyPomodoroTimerMigrations(this IHost host)
        {
            var context = host.Services.GetRequiredService<WachmanDbContext>();
            context.Database.Migrate();
        }
    }

}
