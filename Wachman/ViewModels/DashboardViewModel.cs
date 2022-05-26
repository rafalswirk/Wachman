using DataModels.Jobs;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wachman.ViewModels
{
    public class DashboardViewModel: ObservableObject
    {
        private List<Job> _dailyJobs;
        public List<Job> DailyJobs
        {
            get =>_dailyJobs;
            set => SetProperty(ref _dailyJobs, value);
        }

    }
}
