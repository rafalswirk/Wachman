using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTrackingService.TimeCampAPI;
using Wachman.Tests.TimeTrackingServiceTests.Mocks;
using Xunit;

namespace Wachman.Tests.TimeTrackingServiceTests.TimeCampTests
{
    public  class CheckJsonParsingLogic
    {
        [Fact]
        public async Task GetDailyJobsTest()
        {
            var client = new RestClientMock();
            var getDailyJobs = new GetDailyJobs(client.GetRestClient());

            var jobs = await getDailyJobs.ExecuteAsync();

            jobs.Should().NotBeNull();
            jobs.Count.Should().Be(11);
            jobs.First().GetType().Properties().Count().Should().Be(6);
            jobs.First().Name.Should().Be("Wachman");
            jobs.First().Description.Should().Be("Demo");
            jobs.First().Duration.Should().Be(new TimeSpan(1, 0, 26));
            jobs.First().Start.Should().Be(new DateTime(2022, 5, 26, 5, 19, 34));
            jobs.First().Stop.Should().Be(new DateTime(2022, 5, 26, 6, 20, 0));
            jobs.First().IsRunning.Should().Be(false);
        }
    }
}
