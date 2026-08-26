
using FocusForge.PomodoroTimer.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FocusForge.PomodoroTimer.UI
{
    public static class Extension
    {
        public static void AddPomodoroTimer(this IServiceCollection services)
        {
            services.AddPomodoroTimerCore();
            services.AddSingleton<PomodoroViewModel>();
        }

        public static void InitializePomodoroTimerModule(this IHost host)
        {
            host.ApplyPomodoroTimerMigrations();
        }
    }

}
