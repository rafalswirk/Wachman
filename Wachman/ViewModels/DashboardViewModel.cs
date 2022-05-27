using DataModels.Jobs;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTrackingService.TimeCampAPI;
using TimeTrackingService.TimeCampAPI.Client;

namespace Wachman.ViewModels
{
    public class DashboardViewModel : ObservableObject
    {
        private const bool HardcodedValues = true;

        private List<Job> _dailyJobs;
        public List<Job> DailyJobs
        {
            get => _dailyJobs;
            set => SetProperty(ref _dailyJobs, value);
        }

        internal async Task OnLoaded()
        {
            if (HardcodedValues)
            {
                var dataItems = new List<Job>()
                {
                    new Job{ Name = "ProgrammingKata", Description = "Making fancy algorithm", Start = new DateTime(2021, 11, 1, 12, 3, 22), Stop = new DateTime(2021, 11, 1, 13, 3, 22), Duration = TimeSpan.FromMinutes(25)},
                    new Job{ Name = "Mailing", Description = "Mail to office", Start = new DateTime(2021, 11, 1, 13, 3, 22), Stop = new DateTime(2021, 11, 1, 13, 3, 22), Duration = TimeSpan.FromMinutes(72)},
                    new Job{ Name = "Tea time", Description = "Green tea", Start = new DateTime(2021, 11, 1, 12, 3, 22), Stop = new DateTime(2021, 11, 1, 13, 3, 22), Duration = TimeSpan.FromSeconds(320)}
                };

                DailyJobs = dataItems;
                return;
            }

            var dailyJobs = new GetDailyJobs(TimeCampApiClient.Instance);
            DailyJobs = await dailyJobs.ExecuteAsync();
        }

    }
}
