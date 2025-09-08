using FakeItEasy;
using FocusForge.TimeTracker.TimeCamp.ApiCommunication;
using FocusForge.TimeTracker.TimeCamp.ApiCommunication.Client;
using FocusForge.TimeTracker.TimeCamp.Queries.Handlers;
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
            var getDailyJobs = new DailyJobsQueryHandler(apiClient);

            var result = await getDailyJobs.HandleAsync(new TimeTracker.TimeCamp.Queries.DailyJobsQuery());

            result.ShouldNotBeNull();
            result.Jobs.Count.ShouldBe(11);
            result.Jobs.First().GetType().GetProperties().Count().ShouldBe(6);
            result.Jobs.First().Name.ShouldBe("Wachman");
            result.Jobs.First().Description.ShouldBe("Demo");
            result.Jobs.First().Duration.ShouldBe(new TimeSpan(1, 0, 26));
            result.Jobs.First().Start.ShouldBe(new DateTime(2022, 5, 26, 5, 19, 34));
            result.Jobs.First().Stop.ShouldBe(new DateTime(2022, 5, 26, 6, 20, 0));
            result.Jobs.First().IsRunning.ShouldBe(false);
        }
    }
}
