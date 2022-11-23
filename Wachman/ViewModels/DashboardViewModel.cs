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
        private const bool HardcodedValues = true;
        private readonly ITimeTrackingService _timeTrackingService;
        private PomodoroViewModel _promodoroViewModel = new();
        private CurrentDayViewModel _currentDayViewModel = new();

        private List<Job> _dailyJobs;
        public List<Job> DailyJobs
        {
            get => _dailyJobs;
            set => SetProperty(ref _dailyJobs, value);
        }

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
            _timeTrackingService = timeTrackingService;
            ChangeJobStatus = new RelayCommand<Job>(job => 
            {
                job.IsRunning = true;
            });
            SwitchToCurrentDay = new RelayCommand(() => SelectedViewModel = _currentDayViewModel);
            SwitchPomodoroTimer = new RelayCommand(() => SelectedViewModel = _promodoroViewModel);
        }

        internal async Task OnLoaded()
        {
            await _timeTrackingService.InitializeAsync();
            DailyJobs = await _timeTrackingService.GetDailyJobsAsync();
            SelectedViewModel = _currentDayViewModel;
        }

    }
}
