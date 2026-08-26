using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.Activities.Core.Monitoring
{
    public class TimerTrigger : IActivityMonitorTrigger
    {
        private System.Timers.Timer? _timer;

        public TimeSpan Interval => TimeSpan.FromSeconds(1);

        public event EventHandler OnTrigger;

        public void Start()
        {
            if (_timer is not null)
            {
                if (!_timer.Enabled)
                {
                    _timer.Start();
                }
                return;
            }

            _timer = new System.Timers.Timer(Interval);
            _timer.Elapsed += (s, e) =>
            {
                OnTrigger?.Invoke(this, EventArgs.Empty);
            };
            _timer.AutoReset = true;
            _timer.Start();
        }

        public void Stop()
        {
            if (_timer is null)
                return;

            _timer.Stop();
            _timer.Dispose();
            _timer = null;
        }

        public void Trigger()
        {
            OnTrigger?.Invoke(this, EventArgs.Empty);
        }
    }
}
