using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.TimeCamp.ApiCommunication
{
    internal interface ITimeTrackingCommand<T>
    {
        Task<T> ExecuteAsync();
    }
}
