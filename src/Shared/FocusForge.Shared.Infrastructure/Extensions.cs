using FocusForge.Shared.Abstractions.Commands;
using FocusForge.Shared.Abstractions.Queries;
using FocusForge.Shared.Infrastructure.Commands;
using FocusForge.Shared.Infrastructure.Events;
using FocusForge.Shared.Infrastructure.Queries;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.Shared.Infrastructure
{
    public static class Extensions
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<IQueryDispatcher, QueryDispatcher>();
            services.AddEvents();
        }
    }
}
