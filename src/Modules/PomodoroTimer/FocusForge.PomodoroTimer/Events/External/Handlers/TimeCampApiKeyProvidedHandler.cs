using FocusForge.Shared.Abstractions.Events;
using FocusForge.TimeTracker.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.PomodoroTimer.Events.External.Handlers
{
    internal class TimeCampApiKeyProvidedHandler : IEventHandler<TimeCampApiKeyProvided>
    {
        public Task HandleAsync(TimeCampApiKeyProvided @event)
        {
            throw new NotImplementedException();
        }
    }
}
