using FakeItEasy;
using FocusForge.TimeTracker.TimeCamp.ApiCommunication;
using FocusForge.TimeTracker.TimeCamp.ApiCommunication.Client;
using FocusForge.UnitTests.TimeTrackingServiceTests.TimeCampTests.Mocks;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FocusForge.UnitTests.TimeTrackingServiceTests.TimeCampTests
{
    public class CheckJsonParsingLogic
    {
        [Fact]
        public async Task GetDailyJobsTest()
        {
            var apiClient = A.Fake<ITimeCampApiClient>();
            A.CallTo(() => apiClient.Client).Returns(new RestClientMock().GetRestClient());
            var client = new RestClientMock();
            var getDailyJobs = new GetDailyJobs(apiClient);

            var jobs = await getDailyJobs.ExecuteAsync();

            jobs.ShouldNotBeNull();
            jobs.Count.ShouldBe(11);
            jobs.First().GetType().GetProperties().Count().ShouldBe(6);
            jobs.First().Name.ShouldBe("Wachman");
            jobs.First().Description.ShouldBe("Demo");
            jobs.First().Duration.ShouldBe(new TimeSpan(1, 0, 26));
            jobs.First().Start.ShouldBe(new DateTime(2022, 5, 26, 5, 19, 34));
            jobs.First().Stop.ShouldBe(new DateTime(2022, 5, 26, 6, 20, 0));
            jobs.First().IsRunning.ShouldBe(false);
        }
    }
}
