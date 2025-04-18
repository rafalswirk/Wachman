using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTrackingService.TimeTrackingServiceCommand
{
    internal interface ITimeTrackingCommand<T>
    {
        Task<T> ExecuteAsync();
    }
}
