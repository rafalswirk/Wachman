using FocusForge.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.Messages.Events
{
    public record TimeCampApiKeyProvided(string ApiKey) : IEvent;
}
