using FocusForge.TimeTracker.DAL;
using FocusForge.TimeTracker.DAL.Respositories;
using FocusForge.TimeTracker.Integrations.TimeCamp;
using FocusForge.TimeTracker.Repositories;
using FocusForge.TimeTracker.Services.TimeTracking;
using FocusForge.TimeTracker.TimeCamp;
using FocusForge.TimeTracker.TimeCamp.ApiCommunication.Client;
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
            services.AddCommands();
            services.AddScoped<TimeCampApiFactory>();
            services.AddScoped<ITimeTrackerSettingsRepository, TimeTrackerSettingsRepository>();
            services.AddScoped<TimeTrackerDbContext>();
            services.AddScoped<ITasksWriter, TimeCampTasksDatabaseWriter>();
            services.AddScoped<ITasksLoader, TimeCampTasksLoader>();
            services.AddScoped<ITasksReader, TimeCampTasksDatabaseReader>();
        }

        public static void ApplyTimeTrackerMigrations(this IHost host)
        {
            var context = host.Services.GetRequiredService<TimeTrackerDbContext>();
            context.Database.Migrate();
        }

        public static void InitializeTimeCampClient(this IHost host)
        {
            var settings = host.Services.GetRequiredService<ITimeTrackerSettingsRepository>();
            TimeCampApiClient.Initialize(settings.TimeCampApiKey);

        }
    }
}
