
using FocusForge.TimeTracker.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;

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
