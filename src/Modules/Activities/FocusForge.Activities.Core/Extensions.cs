using FocusForge.Activities.Core.Monitoring;
using Microsoft.Extensions.DependencyInjection;

namespace FocusForge.Activities.Core
{
    public static class Extensions
    {
        public static void AddActivitiesCore(this IServiceCollection services)
        {
            services.AddScoped<Monitoring.IActivityReader, Monitoring.ActivityReader>();
            services.AddScoped<ActivityMonitor>();
            services.AddScoped<IActivityMonitorTrigger, TimerTrigger>();
            //services.
        }
    }
}
