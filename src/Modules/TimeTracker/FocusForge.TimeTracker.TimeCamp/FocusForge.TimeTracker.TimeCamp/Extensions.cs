using FocusForge.Abstractions.Commands;
using FocusForge.TimeTracker.TimeCamp.Commands;
using FocusForge.TimeTracker.TimeCamp.Commands.Handlers;
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
            services.AddScoped<ICommandHandler<StartJobCommand>, StartJobCommandHandler>();
        }
    }
}
