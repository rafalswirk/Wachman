using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.Activities.Core.Monitoring
{
    internal class ManualTrigger : IActivityMonitorTrigger
    {
        public TimeSpan Interval => TimeSpan.Zero;

        public event EventHandler OnTrigger;

        public void Start()
        {
        }

        public void Stop()
        {
        }

        public void Trigger()
        {
            OnTrigger?.Invoke(this, EventArgs.Empty);
        }
    }
}
