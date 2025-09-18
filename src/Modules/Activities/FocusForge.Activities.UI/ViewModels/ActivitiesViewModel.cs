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
        private string _currentActivity;
        private HashSet<string> _allActivitiesBuffer;
        private System.Timers.Timer _timer;

        public string CurrentActivity
        {
            get { return _currentActivity; }
            set { SetProperty(ref _currentActivity, value); }
        }

        private string _allActivities;
        public string AllActivities
        {
            get { return _allActivities; }
            set { SetProperty(ref _allActivities, value); }
        }


        public ActivitiesViewModel()
        {
            _allActivitiesBuffer = new HashSet<string>();
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += (s, e) =>
            {
                CurrentActivity = ActivityReader.GetActiveWindowTitle();
                if(!_allActivitiesBuffer.Contains(CurrentActivity))
                {
                    _allActivitiesBuffer.Add(CurrentActivity);
                    AllActivities = string.Join(Environment.NewLine, _allActivitiesBuffer.ToArray());
                }
            };
            _timer.Start();
        }
    }
}
