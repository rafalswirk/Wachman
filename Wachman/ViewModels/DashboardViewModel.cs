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

namespace Wachman.ViewModels
{
    public class DashboardViewModel : ObservableObject
    {
        private PomodoroViewModel _promodoroViewModel = new();
        private CurrentDayViewModel _currentDayViewModel;

        private ObservableObject _selectedViewModel;
        public ObservableObject SelectedViewModel
        {
            get => _selectedViewModel;
            set => SetProperty(ref _selectedViewModel, value);
        }

        public ICommand ChangeJobStatus { get; private set; }
        public ICommand SwitchToCurrentDay { get; set; }
        public ICommand SwitchPomodoroTimer { get; set; }

        public DashboardViewModel(ITimeTrackingService timeTrackingService)
        {
            _currentDayViewModel = new CurrentDayViewModel(timeTrackingService);
            ChangeJobStatus = new RelayCommand<Job>(job => 
            {
                job.IsRunning = true;
            });
            SwitchToCurrentDay = new RelayCommand(() => SelectedViewModel = _currentDayViewModel);
            SwitchPomodoroTimer = new RelayCommand(() => SelectedViewModel = _promodoroViewModel);
        }

        internal async Task OnLoaded()
        {
            await _currentDayViewModel.InitializeAsync();
            SelectedViewModel = _promodoroViewModel;
        }

    }
}
