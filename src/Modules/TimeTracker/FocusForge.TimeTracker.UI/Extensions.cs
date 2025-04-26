
using Microsoft.Extensions.DependencyInjection;
using Wachman.ViewModels;

namespace FocusForge.TimeTracker.UI
{
    public static class Extensions
    {
        public static void AddTimeTracker(this IServiceCollection services)
        {
            services.AddSingleton<CurrentDayViewModel>();
        }
    }

}
