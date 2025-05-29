using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using FocusForge.DataModels.Jobs;
using FocusForge.TimeTracker;
using FocusForge.TimeTracker.Integrations.TimeCamp;

namespace FocusForge.TimeTracker.UI.ViewModels
{
    public class CurrentDayViewModel : ObservableObject
    {
        private ITimeTrackingService _timeTrackingService;


        private List<Job> _dailyJobs;
        public List<Job> DailyJobs
        {
            get => _dailyJobs;
            set => SetProperty(ref _dailyJobs, value);
        }

        public IRelayCommand CreateNewJob { get; set; }

        public IAsyncRelayCommand OnLoad { get; set; }

        public CurrentDayViewModel(TimeCampApiFactory timeCampApiFactory)
        {
            _timeTrackingService = timeCampApiFactory.Create();
            OnLoad = new AsyncRelayCommand(InitializeAsync);
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            CreateNewJob = new RelayCommand(() => 
            {
                _timeTrackingService.StartNewJobAsync();
            });
        }

        internal async Task InitializeAsync()
        {
            await _timeTrackingService.InitializeAsync();
            DailyJobs = await _timeTrackingService.GetDailyJobsAsync();
        }
    }
}
