using FocusForge.TimeTracker.DAL;
using FocusForge.TimeTracker.DAL.Respositories;
using FocusForge.TimeTracker.Repositories;
using FocusForge.TimeTracker.TimeCampAPI.DummyAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

        public static void ApplyTimeTrackerMigrations(this IHost host)
        {
            var context = host.Services.GetRequiredService<TimeTrackerDbContext>();
            context.Database.Migrate();
        }
    }
}
