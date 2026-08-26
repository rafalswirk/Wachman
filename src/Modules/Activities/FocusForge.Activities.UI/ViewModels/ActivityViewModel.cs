using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.Activities.UI.ViewModels
{
    public class ActivityViewModel : ObservableObject
    {
        public string Activity { get; set; } = string.Empty;
        
        private TimeSpan _time = TimeSpan.Zero;
        public TimeSpan Time { get => _time; set => SetProperty(ref _time, value); }
     
        public ushort Percentage { get; set; } = 0;
        public ObservableCollection<ActivityViewModel> InnerActivities { get; set; } = [];
    }
}
