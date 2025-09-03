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
using FocusForge.Abstractions.Commands;
using FocusForge.TimeTracker.TimeCamp.Commands;

namespace FocusForge.TimeTracker.UI.ViewModels
{
    public class CurrentDayViewModel : ObservableObject
    {
        private ITimeTrackingService _timeTrackingService;


        private List<Job> _dailyJobs;
        private readonly ITasksReader _tasksReader;
        private readonly ICommandDispatcher _commandDispatcher;

        public List<Job> DailyJobs
        {
            get => _dailyJobs;
            set => SetProperty(ref _dailyJobs, value);
        }
        
        private IReadOnlyCollection<TimeTrackerTask> _tasks;
        public IReadOnlyCollection<TimeTrackerTask> Tasks { get => _tasks; private set => SetProperty(ref _tasks, value); }

        private TimeTrackerTask _taskToStart;

        public TimeTrackerTask TaskToStart
        {
            get { return _taskToStart; }
            set { SetProperty(ref _taskToStart, value); }
        }

        public IRelayCommand CreateNewJob { get; set; }
        public IAsyncRelayCommand<Job> RestartJob { get; set; }
        public IAsyncRelayCommand<Job> StopJob { get; set; }

        public IAsyncRelayCommand OnLoad { get; set; }

        public CurrentDayViewModel(TimeCampApiFactory timeCampApiFactory, ITasksReader tasksReader, ICommandDispatcher commandDispatcher)
        {
            _timeTrackingService = timeCampApiFactory.Create();
            _tasksReader = tasksReader;
            _commandDispatcher = commandDispatcher;
            OnLoad = new AsyncRelayCommand(InitializeAsync);
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            CreateNewJob = new AsyncRelayCommand(async () => 
            {
                await _commandDispatcher.SendAsync(new StartJobCommand(TaskToStart.ExternalId));
                await RefreshDailyJobs();

            });

            RestartJob = new AsyncRelayCommand<Job>(async (x) => 
            {
                
                await _commandDispatcher.SendAsync(new StartJobCommand(x.Id));
                await RefreshDailyJobs();
            });
            
            StopJob = new AsyncRelayCommand<Job>(async (x) =>
            {
                //await _commandDispatcher.SendAsync(new StopJobCommand());
                await RefreshDailyJobs();
            });
        }

        internal async Task InitializeAsync()
        {
            await _timeTrackingService.InitializeAsync();
            await RefreshDailyJobs();
            Tasks = await _tasksReader.ReadAsync();
        }

        private async Task RefreshDailyJobs()
        {
            DailyJobs = await _timeTrackingService.GetDailyJobsAsync();
        }
    }
}
