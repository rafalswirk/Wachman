
using Microsoft.Extensions.DependencyInjection;
using Wachman.ViewModels;

namespace FocusForge.PomodoroTimer.UI
{
    public static class Extension
    {
        public static void AddPomodoroTimer(this IServiceCollection services)
        {
            services.AddPomodoroTimerCore();
            services.AddSingleton<PomodoroViewModel>();
        }
    }

}
