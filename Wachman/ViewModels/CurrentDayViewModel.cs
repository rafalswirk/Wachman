using DataModels.Jobs;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTrackingService;

namespace Wachman.ViewModels
{
    public class CurrentDayViewModel: ObservableObject
    {
        private ITimeTrackingService _timeTrackingService;


        private List<Job> _dailyJobs;
        public List<Job> DailyJobs
        {
            get => _dailyJobs;
            set => SetProperty(ref _dailyJobs, value);
        }

        public CurrentDayViewModel(ITimeTrackingService timeTrackingService)
        {
            _timeTrackingService = timeTrackingService;
        }

        internal async Task InitializeAsync()
        {
            await _timeTrackingService.InitializeAsync();
            DailyJobs = await _timeTrackingService.GetDailyJobsAsync();
        }
    }
}
