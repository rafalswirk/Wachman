using DataModels.Jobs;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeTrackingService;
using TimeTrackingService.TimeCampAPI;
using TimeTrackingService.TimeCampAPI.Client;
using Wachman.Utils.DataStorage;

namespace Wachman.ViewModels
{
    public class DashboardViewModel : ObservableObject
    {
        private PomodoroViewModel _promodoroViewModel = new();
        private CurrentDayViewModel _currentDayViewModel;
        private SettingsViewModel _settingsViewModel; 

        private ObservableObject _selectedViewModel;

        public ObservableObject SelectedViewModel
        {
            get => _selectedViewModel;
            set => SetProperty(ref _selectedViewModel, value);
        }

        public ICommand ChangeJobStatus { get; private set; }
        public ICommand SwitchToCurrentDay { get; set; }
        public ICommand SwitchPomodoroTimer { get; set; }
        public ICommand SwitchToSettings { get; set; }

        public DashboardViewModel(ITimeTrackingService timeTrackingService, IApiKeyProvider apiKeyProvider)
        {
            _settingsViewModel = new SettingsViewModel(apiKeyProvider);
            _currentDayViewModel = new CurrentDayViewModel(timeTrackingService);
            ChangeJobStatus = new RelayCommand<Job>(job => 
            {
                job.IsRunning = true;
            });
            SwitchToCurrentDay = new RelayCommand(() => SelectedViewModel = _currentDayViewModel);
            SwitchPomodoroTimer = new RelayCommand(() => SelectedViewModel = _promodoroViewModel);
            SwitchToSettings = new RelayCommand(() => SelectedViewModel = _settingsViewModel);
        }

        internal async Task OnLoaded()
        {
            await _currentDayViewModel.InitializeAsync();
            SelectedViewModel = _promodoroViewModel;
        }

    }
}
