using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusForge.Shared.Abstractions.Commands;

namespace FocusForge.TimeTracker.TimeCamp.Commands
{
    public record StopJobCommand() : ICommand;
}
