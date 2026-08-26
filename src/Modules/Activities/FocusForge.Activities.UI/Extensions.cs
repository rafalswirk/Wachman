
using FocusForge.Activities.Core;
using FocusForge.Activities.UI.Services;
using FocusForge.Activities.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FocusForge.Activities.UI
{
    public static class Extensions
    {
        public static void AddActivities(this IServiceCollection serviceProvider)
        {
            serviceProvider.AddActivitiesCore();
            serviceProvider.AddSingleton<IRawActivitiesExportService, RawActivitiesExportService>();
            serviceProvider.AddSingleton<ActivitiesViewModel>();
        }

        public static void InitializeActivitiesModule(this IServiceProvider serviceProvider)
        {
            serviceProvider.GetRequiredService<ActivitiesViewModel>();
        }
    }

}
