using DataModels.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTrackingService.DummyAPI
{
    internal class DummyTrackingService : ITimeTrackingService
    {
        public Task<bool> InitializeAsync() => Task.FromResult(true);

        public Task<List<Job>> GetDailyJobsAsync() => Task.FromResult(new List<Job>()
        {
            new Job{ Name = "ProgrammingKata", Description = "Making fancy algorithm", Start = new DateTime(2021, 11, 1, 12, 3, 22), Stop = new DateTime(2021, 11, 1, 13, 3, 22), Duration = TimeSpan.FromMinutes(25)},
            new Job{ Name = "Mailing", Description = "Mail to office", Start = new DateTime(2021, 11, 1, 13, 3, 22), Stop = new DateTime(2021, 11, 1, 13, 3, 22), Duration = TimeSpan.FromMinutes(72)},
            new Job{ Name = "Tea time", Description = "Green tea", Start = new DateTime(2021, 11, 1, 12, 3, 22), Stop = new DateTime(2021, 11, 1, 13, 3, 22), Duration = TimeSpan.FromSeconds(320)}
        });

        public Task<string> GetCurrentJobName() => Task.FromResult("Pet project development");
    }
}
