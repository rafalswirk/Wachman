using FocusForge.Shared.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.TimeCamp.Commands
{
    public record StartJobCommand(long TaskId) : ICommand;
}
