using Microsoft.Extensions.DependencyInjection;

namespace FocusForge.Activities.Core
{
    public static class Extensions
    {
        public static void AddActivitiesCore(this IServiceCollection services)
        {
            services.AddSingleton<Monitoring.IActivityReader, Monitoring.ActivityReader>();
        }
    }
}
