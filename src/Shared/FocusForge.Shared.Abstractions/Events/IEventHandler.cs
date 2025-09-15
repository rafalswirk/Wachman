using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.Shared.Abstractions.Events
{
    public interface IEventHandler<in TEvent> : IEvent where TEvent :class, IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}
