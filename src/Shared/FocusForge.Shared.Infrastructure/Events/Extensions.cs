using FocusForge.Shared.Abstractions.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.Shared.Infrastructure.Events
{
    public static class Extensions
    {
        public static IServiceCollection AddEvents(this IServiceCollection services)
        {
            services.AddSingleton<IEventDispatcher, EventDispatcher>();
            
            return services;
        }
    }
}
