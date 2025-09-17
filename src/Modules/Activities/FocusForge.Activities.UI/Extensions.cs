
using FocusForge.Activities.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace FocusForge.Activities.UI
{
    public static class Extensions
    {
        public static void AddActivities(this IServiceCollection serviceProvider)
        {
            serviceProvider.AddSingleton<ActivitiesViewModel>();
        }
    }

}
