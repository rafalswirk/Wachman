using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FocusForge.Shared.Abstractions.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FocusForge.Desktop.ViewModels
{
    public class SettingsViewModel : ObservableObject
    {
        private bool _isMessageVisible;
        private bool _timeCampIntegrationEnabled;

        public bool TimeCampIntegrationEnabled
        {
            get => _timeCampIntegrationEnabled;
            set => SetProperty(ref _timeCampIntegrationEnabled, value);
        }
        public ICommand SaveTimeCampSettings { get; set; }
        public bool IsMessageVisible
        {
            get => _isMessageVisible;
            set => SetProperty(ref _isMessageVisible, value);
        }

        public IEnumerable<IModuleSettings> Settings { get; set; }

        public SettingsViewModel(IModuleSettings timeTrackerSettings)
        {
            Settings = new List<IModuleSettings>() { timeTrackerSettings };
            Initialize();
        }

        private void Initialize()
        {
            IsMessageVisible = false;
            SaveTimeCampSettings = new RelayCommand(() =>
            {
                IsMessageVisible = true;
            });
        }
    }
}
