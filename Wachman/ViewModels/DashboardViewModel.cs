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
        private List<Job> _dailyJobs;
        public List<Job> DailyJobs
        {
            get => _dailyJobs;
            set => SetProperty(ref _dailyJobs, value);
        }

        public ICommand ChangeJobStatus { get; private set; }

        public DashboardViewModel(ITimeTrackingService timeTrackingService)
        {
            _timeTrackingService = timeTrackingService;
            ChangeJobStatus = new RelayCommand<Job>(job => 
            {
                job.IsRunning = true;
            });
        }

        internal async Task OnLoaded()
        {
            await _timeTrackingService.InitializeAsync();
            DailyJobs = await _timeTrackingService.GetDailyJobsAsync();
        }

    }
}
