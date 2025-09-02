using FocusForge.Abstractions.Commands;
using FocusForge.Abstractions.Queries;
using FocusForge.TimeTracker.TimeCamp.ApiCommunication.Client;
using FocusForge.TimeTracker.TimeCamp.Commands;
using FocusForge.TimeTracker.TimeCamp.Commands.Handlers;
using FocusForge.TimeTracker.TimeCamp.DAL.DTO;
using FocusForge.TimeTracker.TimeCamp.Queries;
using FocusForge.TimeTracker.TimeCamp.Queries.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.TimeCamp
{
    public static class Extensions
    {
        public static void AddCommands(this IServiceCollection services)
        {
            services.AddSingleton<ITimeCampApiClient, TimeCampApiClient>();
            services.AddScoped<ICommandHandler<StartJobCommand>, StartJobCommandHandler>();
            services.AddScoped<IQueryHandler<AvailableTaskaQuery, IEnumerable<TaskInfoDTO>>, AvailableTaskaQueryHandler>();
            services.AddScoped<IQueryHandler<TimerStatusQuery, TimerStatusDTO>, TimerStatusQueryHandler>();
            services.AddScoped<IQueryHandler<DailyJobsQuery, DailyJobsDTO>, DailyJobsQueryHandler>();
            services.AddScoped<IQueryHandler<JobTimerRunningQuery, JobTimerRunningDTO>, JobTimerRunningQueryHandler>();
        }
    }
}
