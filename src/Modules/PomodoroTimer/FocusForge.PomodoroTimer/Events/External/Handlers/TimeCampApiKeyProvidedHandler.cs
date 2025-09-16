using FocusForge.PomodoroTimer.Repositories;
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
        private readonly ITimeCampIntegrationRepository _integrationRepository;

        public TimeCampApiKeyProvidedHandler(ITimeCampIntegrationRepository integrationRepository)
        {
            _integrationRepository = integrationRepository;
        }

        public Task HandleAsync(TimeCampApiKeyProvided @event)
        {
            _integrationRepository.SaveTimeCampIntegrationData(new Models.TimeCampIntegrationData { ApiKey = @event.ApiKey });
            return Task.CompletedTask;
        }
    }
}
