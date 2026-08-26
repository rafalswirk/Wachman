using FakeItEasy;
using FocusForge.Activities.Core.Monitoring;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FocusForge.UnitTests.ActivitiesTests
{
    public class ActivityMonitorTestFixture
    {
        [Fact]
        public void GetActivities_ReturnsListOfActivities()
        {
            var dummyReader = A.Fake<IActivityReader>();
            A.CallTo(() => dummyReader.ReadActivity())
                .ReturnsNextFromSequence(new ActiveProcessInfo("Notepad.exe", "*test.txt - Notepad"), new ActiveProcessInfo("opera.exe", "Sign in to GitHub · GitHub - Opera"));
            var manualTrigger = new ManualTrigger();
            var monitor = new ActivityMonitor(dummyReader, manualTrigger);
            monitor.Start();
            manualTrigger.Trigger();
            manualTrigger.Trigger();
            monitor.Stop();

            var activities = monitor.GetActivities();


            activities.Count.ShouldBe(2);
            activities.First().Activity.Executable.ShouldBe("Notepad.exe");
            activities.First().Activity.ApplicationName.ShouldBe("Notepad");
            activities.First().Activity.ActivityDescription.ShouldBe("test.txt");
            activities.First().Activity.InnerActivityDescription.ShouldBe(string.Empty);
            activities.First().Ticks.ShouldBe(1);

            activities.Last().Activity.Executable.ShouldBe("opera.exe");
            activities.Last().Activity.ApplicationName.ShouldBe("Opera");
            activities.Last().Activity.ActivityDescription.ShouldBe("GitHub");
            activities.Last().Activity.InnerActivityDescription.ShouldBe("Sign in to GitHub");
            activities.Last().Ticks.ShouldBe(1);
        }
    }
}
