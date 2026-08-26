using FocusForge.Activities.Core.Monitoring.ProcessAnalyse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.Activities.Core.Monitoring
{
    public class ActivityMonitor
    {
        private Dictionary<string, List<MonitoredActivity>> _activityStorage = [];
        private ProcessInfoToActivityConverter _activityConverter = new();
        private readonly IActivityReader _activityReader;
        private readonly IActivityMonitorTrigger _trigger;
        private bool _isStarted;


        public event EventHandler<ActivityReadEventArgs> OnActivityRead;

        public ActivityMonitor(IActivityReader activityReader, IActivityMonitorTrigger trigger)
        {
            _activityConverter.RegisterProcessDescription(new NotepadDescription());
            _activityConverter.RegisterProcessDescription(new OperaProcessDescription());
            _activityConverter.RegisterProcessDescription(new WhiteStarUml());
            _activityReader = activityReader;
            _trigger = trigger;
        }

        public IReadOnlyCollection<MonitoredActivity> GetActivities()
            => [.. _activityStorage.Values.SelectMany(x => x)];

        public void Start()
        {
            if (_isStarted)
                return;

            _trigger.OnTrigger += _trigger_OnTrigger;
            _trigger.Start();
            _isStarted = true;
        }

        public void Stop()
        {
            if (!_isStarted)
                return;

            _trigger.OnTrigger -= _trigger_OnTrigger;
            _trigger.Stop();
            _isStarted = false;
        }

        private void _trigger_OnTrigger(object? sender, EventArgs e)
        {
            var rawActivity = _activityReader.ReadActivity();
            var activity = _activityConverter.Convert(rawActivity);
            if (_activityStorage.ContainsKey(activity.Executable))
            {
                var executableActivities = _activityStorage[activity.Executable];
                var storedActivity = executableActivities.FirstOrDefault(a => a.Activity == activity);
                if (storedActivity == null)
                {
                    executableActivities.Add(new MonitoredActivity { Activity = activity, Ticks = 1 });
                    OnActivityRead?.Invoke(this, new ActivityReadEventArgs { Activity = activity, Time = _trigger.Interval });
                    return;
                }
                storedActivity.Ticks += 1;
            }
            else
            {
                _activityStorage.Add(activity.Executable,
                    [
                        new MonitoredActivity { Activity = activity, Ticks = 1 }
                    ]);
            }
            OnActivityRead?.Invoke(this, new ActivityReadEventArgs { Activity = activity, Time = _trigger.Interval });
        }
    }
}
  