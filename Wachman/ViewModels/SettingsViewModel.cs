using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Wachman.Utils.DataStorage;

namespace Wachman.ViewModels
{
    public class SettingsViewModel : ObservableObject
    {
        private IApiKeyProvider _keyProvider;
        private bool _isMessageVisible;
        private bool _timeCampIntegrationEnabled;

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
        public SettingsViewModel(IApiKeyProvider keyProvider)
        {
            _keyProvider = keyProvider;
            Initialize();
        }

        private void Initialize()
        {
            IsMessageVisible = false;
            ApiKey = _keyProvider.GetKey();
            TimeCampIntegrationEnabled = !string.IsNullOrEmpty(ApiKey);
            SaveTimeCampSettings = new RelayCommand(() =>
            {
                if (!TimeCampIntegrationEnabled)
                {
                    _keyProvider.SetKey(ApiKey);
                }
                _keyProvider.SetKey(ApiKey);
                IsMessageVisible = true;
            });
        }
    }
}
