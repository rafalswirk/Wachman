using DataModels.Jobs;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public DashboardViewModel(ITimeTrackingService timeTrackingService)
        {
            _timeTrackingService = timeTrackingService;
        }

        internal async Task OnLoaded()
        {
            await _timeTrackingService.InitializeAsync();
            DailyJobs = await _timeTrackingService.GetDailyJobsAsync();
        }

    }
}
