using CommunityToolkit.Mvvm.ComponentModel;
using FocusForge.TimeTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.UI.Models
{
    public class SyncTaskItem : ObservableObject
    {
        private bool _import;
        public bool Import 
        { 
            get => _import; 
            set => SetProperty(ref _import, value); 
        }
        public TimeTrackerTask Task { get; set; }
    }
}
