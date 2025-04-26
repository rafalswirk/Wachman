
using FocusForge.PomodoroTimer.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;

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
