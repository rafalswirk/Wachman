using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using FocusForge.DataModels.Jobs;
using FocusForge.TimeTracker.Integrations.TimeCamp;
using FocusForge.TimeTracker.Services.TimeTracking;
using FocusForge.TimeTracker.Entities;

namespace FocusForge.TimeTracker.UI.ViewModels
{
    public class CurrentDayViewModel : ObservableObject
    {
        private ITimeTrackingService _timeTrackingService;


        private List<Job> _dailyJobs;
        private readonly ITasksReader _tasksReader;

        public List<Job> DailyJobs
        {
            get => _dailyJobs;
            set => SetProperty(ref _dailyJobs, value);
        }
        
        private IReadOnlyCollection<TimeTrackerTask> _tasks;
        public IReadOnlyCollection<TimeTrackerTask> Tasks { get => _tasks; private set => SetProperty(ref _tasks, value); }
        
        public IRelayCommand CreateNewJob { get; set; }

        public IAsyncRelayCommand OnLoad { get; set; }

        public CurrentDayViewModel(TimeCampApiFactory timeCampApiFactory, ITasksReader tasksReader)
        {
            _timeTrackingService = timeCampApiFactory.Create();
            _tasksReader = tasksReader;
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
            Tasks = await _tasksReader.ReadAsync();
        }
    }
}
