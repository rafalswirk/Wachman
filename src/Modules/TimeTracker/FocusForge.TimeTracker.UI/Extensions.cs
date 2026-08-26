using FocusForge.Shared.Abstractions.Settings;
using FocusForge.TimeTracker;
using FocusForge.TimeTracker.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FocusForge.TimeTracker.UI
{
    public static class Extensions
    {
        public static void AddTimeTracker(this IServiceCollection services)
        {
            services.AddTimeTrackerCore();
            services.AddSingleton<CurrentDayViewModel>();
            services.AddSingleton<SyncTasksViewModel>();
            services.AddScoped<IModuleSettings, TimeTrackerSettingsViewModel>();
        }

        public static void InitializeTimeTrackerModule(this IHost host)
        {
            host.ApplyTimeTrackerMigrations();
            host.InitializeTimeCampClient();
        }
    }

}
