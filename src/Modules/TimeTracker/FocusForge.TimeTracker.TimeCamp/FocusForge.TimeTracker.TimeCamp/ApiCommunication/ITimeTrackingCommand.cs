using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.TimeTrackingServiceCommand
{
    internal interface ITimeTrackingCommand<T>
    {
        Task<T> ExecuteAsync();
    }
}
