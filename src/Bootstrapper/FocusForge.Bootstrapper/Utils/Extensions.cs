using FocusForge.Desktop.Utils.UI.Navigation;
using FocusForge.PomodoroTimer.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.Desktop.Utils
{
    internal static class Extensions
    {
        public static void ConfigureNavigationService(this IHost host)
        {
            var navigationService = host.Services.GetRequiredService<INavigationService>();
            if (navigationService is NavigationService navService)
            {
                navService.Configure(host.Services);
                navService.NavigateTo<PomodoroViewModel>();
            }
        }
    }
}
