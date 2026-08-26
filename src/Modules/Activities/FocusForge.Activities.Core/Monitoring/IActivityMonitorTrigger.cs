using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.Activities.Core.Monitoring
{
    public interface IActivityMonitorTrigger
    {
        event EventHandler OnTrigger;
        TimeSpan Interval { get; }
        void Start();
        void Stop();
        void Trigger();
    }
}
