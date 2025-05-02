using FocusForge.TimeTracker.DummyAPI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker
{
    public static class Extensions
    {
        public static void AddTimeTrackerCore(this IServiceCollection services)
        {
            services.AddSingleton<ITimeTrackingService, DummyTrackingService>();
        }
    }
}
