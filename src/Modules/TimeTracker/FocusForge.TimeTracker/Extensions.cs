using FocusForge.TimeTracker.DAL;
using FocusForge.TimeTracker.DAL.Respositories;
using FocusForge.TimeTracker.DummyAPI;
using FocusForge.TimeTracker.Repositories;
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
            services.AddScoped<ITimeTrackerSettingsRepository, TimeTrackerSettingsRepository>();
            services.AddScoped<TimeTrackerDbContext>();
        }
    }
}
