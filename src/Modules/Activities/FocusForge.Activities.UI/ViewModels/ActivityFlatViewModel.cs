using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.Activities.UI.ViewModels
{
    public class ActivityFlatViewModel: ObservableObject
    {
        public string ApplicationName { get; set; }
        public string ActivityDescription { get; set; }
        public string InnerActivityDescription { get; set; }

        private TimeSpan _activityTime;
        public TimeSpan ActivityTime
        {
            get { return _activityTime; }
            set { SetProperty(ref _activityTime, value); }
        }
    }
}
