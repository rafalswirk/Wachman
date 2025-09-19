using CommunityToolkit.Mvvm.ComponentModel;
using FocusForge.Activities.Core.Monitoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace FocusForge.Activities.UI.ViewModels
{
    public class ActivitiesViewModel : ObservableObject
    {
        private HashSet<string> _allActivitiesBuffer;
        private System.Timers.Timer _timer;

        private Activity _currentActivity;
        public Activity CurrentActivity
        {
            get { return _currentActivity; }
            set { SetProperty(ref _currentActivity, value); }
        }

        private string _allActivities;
        private readonly IActivityReader _activityReader;

        public string AllActivities
        {
            get { return _allActivities; }
            set { SetProperty(ref _allActivities, value); }
        }

        public ActivitiesViewModel(IActivityReader activityReader)
        {
            _activityReader = activityReader;

            _allActivitiesBuffer = new HashSet<string>();
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += (s, e) =>
            {
                CurrentActivity = _activityReader.ReadActivity();
                if (CurrentActivity.ExecutableName == string.Empty)
                    return;

                if (!_allActivitiesBuffer.Contains($"{CurrentActivity.ExecutableName} {CurrentActivity.WindowTitle}"))
                {
                    _allActivitiesBuffer.Add($"{CurrentActivity.ExecutableName} {CurrentActivity.WindowTitle}");
                    AllActivities = string.Join(Environment.NewLine, _allActivitiesBuffer.ToArray());
                }
            };
            _timer.Start();
        }
    }
}
