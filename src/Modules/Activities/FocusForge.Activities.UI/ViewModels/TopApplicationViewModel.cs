using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace FocusForge.Activities.UI.ViewModels
{
    public class TopApplicationViewModel : ObservableObject
    {
        public int Rank { get; set; }
        public string ApplicationName { get; set; } = string.Empty;

        private TimeSpan _timeSpent;
        public TimeSpan TimeSpent
        {
            get { return _timeSpent; }
            set { SetProperty(ref _timeSpent, value); }
        }
    }
}
