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
        private System.Timers.Timer _timer;

        public string CurrentActivity
        {
            get { return _currentActivity; }
            set { SetProperty(ref _currentActivity, value); }
        }

        public ActivitiesViewModel()
        {
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += (s, e) =>
            {
                CurrentActivity = ActivityReader.GetActiveWindowTitle();
            };
            _timer.Start();
        }
    }
}
