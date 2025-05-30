using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FocusForge.DataModels.Core.Settings;
using FocusForge.TimeTracker.Repositories;
using FocusForge.TimeTracker.TimeCamp.ApiCommunication.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FocusForge.TimeTracker.UI.ViewModels
{
    public class TimeTrackerSettingsViewModel : ObservableObject, IModuleSettings
    {
        private bool _isMessageVisible;
        private bool _timeCampIntegrationEnabled;
        private readonly ITimeTrackerSettingsRepository _settingsRepository;

        public string ApiKey { get; set; }
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

        public TimeTrackerSettingsViewModel(ITimeTrackerSettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
            Initialize();
        }

        private void Initialize()
        {
            IsMessageVisible = false;
            ApiKey = _settingsRepository.TimeCampApiKey;
            TimeCampIntegrationEnabled = !string.IsNullOrEmpty(ApiKey);
            SaveTimeCampSettings = new RelayCommand(() =>
            {
                if (!TimeCampIntegrationEnabled)
                {
                    _settingsRepository.SaveTimeCampApiKey("");
                }
                else
                {
                    _settingsRepository.SaveTimeCampApiKey(ApiKey);
                }
                IsMessageVisible = true;
            });
        }
    }
}
