using FocusForge.Activities.Core.Monitoring;
using FocusForge.Activities.Core.Monitoring.ProcessAnalyse;
using FocusForge.Activities.UI.ViewModels;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FocusForge.UnitTests.ActivitiesTests
{
    public class UserActivityToActivityViewModelTestFixture
    {
        // Arrange
        private readonly ObservableCollection<ActivityViewModel> _testActivities = new(
            [
                new ActivityViewModel
                    {
                        Activity = "Opera",
                        Time = TimeSpan.FromMinutes(5),
                        InnerActivities =
                        [
                            new() {
                                Activity = "YouTube",
                                Time = TimeSpan.FromMinutes(5),
                                InnerActivities =
                                [
                                    new ActivityViewModel
                                    {
                                        Activity = "Some Channel",
                                        Time = TimeSpan.FromMinutes(5),
                                    }
                                ]
                            }
                        ]
                    }
            ]);

        [Fact]
        public void UpsertActivityToViewModel_ValidUserActivity_PlaceActivityInHierarchy()
        {
            // Arrange
            var activities = new ObservableCollection<ActivityViewModel>();
            var hierarchyOperations = new ProcessHierarchyOperations();
            var userActivity = new UserActivity("opera.exe", "Opera", "YouTube", "Nick Chapsas");

            // Act
            hierarchyOperations.Upsert(activities, userActivity, TimeSpan.FromMinutes(1));

            // Assert
            activities.Count.ShouldBe(1);
            activities.First().Activity.ShouldBe("Opera");
            activities.First().Time.ShouldBe(TimeSpan.FromMinutes(1));
            activities.First().InnerActivities.First().Activity.ShouldBe("YouTube");
            activities.First().InnerActivities.First().Time.ShouldBe(TimeSpan.FromMinutes(1));
            activities.First().InnerActivities.First().InnerActivities.First().Activity.ShouldBe("Nick Chapsas");
            activities.First().InnerActivities.First().InnerActivities.First().Time.ShouldBe(TimeSpan.FromMinutes(1));
        }

        [Fact]
        public void Upsert2ActivitiesToViewModel_WithDifferentInnerActivity_AddActivityToBottomOfExistingHierarchy()
        {
            // Arrange
            var activities = new ObservableCollection<ActivityViewModel>();
            var hierarchyOperations = new ProcessHierarchyOperations();
            var userActivity = new UserActivity("opera.exe", "Opera", "YouTube", "Nick Chapsas");
            var userActivity2 = new UserActivity("opera.exe", "Opera", "YouTube", "Tim Corey");

            // Act
            hierarchyOperations.Upsert(activities, userActivity, TimeSpan.FromMinutes(1));
            hierarchyOperations.Upsert(activities, userActivity2, TimeSpan.FromMinutes(1));

            // Assert
            activities.Count.ShouldBe(1);
            activities.First().Activity.ShouldBe("Opera");
            activities.First().Time.ShouldBe(TimeSpan.FromMinutes(2));
            activities.First().InnerActivities.First().Activity.ShouldBe("YouTube");
            activities.First().InnerActivities.First().Time.ShouldBe(TimeSpan.FromMinutes(2));
            activities.First().InnerActivities.First().InnerActivities.First().Activity.ShouldBe("Nick Chapsas");
            activities.First().InnerActivities.First().InnerActivities.First().Time.ShouldBe(TimeSpan.FromMinutes(1));
            activities.First().InnerActivities.First().InnerActivities.Last().Activity.ShouldBe("Tim Corey");
            activities.First().InnerActivities.First().InnerActivities.Last().Time.ShouldBe(TimeSpan.FromMinutes(1));
        }

        [Fact]
        public void UpsertWebBrowserActivities_WithDifferentWebPages_AddThemAsChildOfWebBrowserProcess()
        {
            // Arrange
            var hierarchyOperations = new ProcessHierarchyOperations();
            var userActivity = new UserActivity("opera.exe", "Opera", "GitHub", "Sign in to GitHub");

            // Act
            hierarchyOperations.Upsert(_testActivities, userActivity, TimeSpan.FromMinutes(1));

            // Assert
            _testActivities.Count.ShouldBe(1);
            _testActivities.First().Activity.ShouldBe("Opera");
            _testActivities.First().Time.ShouldBe(TimeSpan.FromMinutes(6));
            _testActivities.First().InnerActivities.First().Activity.ShouldBe("YouTube");
            _testActivities.First().InnerActivities.First().Time.ShouldBe(TimeSpan.FromMinutes(5));
            _testActivities.First().InnerActivities.First().InnerActivities.First().Activity.ShouldBe("Some Channel");
            _testActivities.First().InnerActivities.First().InnerActivities.First().Time.ShouldBe(TimeSpan.FromMinutes(5));
            _testActivities.First().InnerActivities.Last().Activity.ShouldBe("GitHub");
            _testActivities.First().InnerActivities.Last().Time.ShouldBe(TimeSpan.FromMinutes(1));
            _testActivities.First().InnerActivities.Last().InnerActivities.First().Activity.ShouldBe("Sign in to GitHub");
            _testActivities.First().InnerActivities.Last().InnerActivities.First().Time.ShouldBe(TimeSpan.FromMinutes(1));
        }

        [Fact]
        public void Upsert2ActivitiesToViewModel_WithDifferentProcessType_AddTwoTopActivitesIntoHierarchy()
        {
            // Arrange
            var activities = new ObservableCollection<ActivityViewModel>();
            var hierarchyOperations = new ProcessHierarchyOperations();
            var userActivity = new UserActivity("opera.exe", "Opera", "YouTube", "Nick Chapsas");
            var userActivity2 = new UserActivity("WhiteStarUML.exe", "WhiteStarUML", "FocusForge.uml", string.Empty);

            // Act
            hierarchyOperations.Upsert(activities, userActivity, TimeSpan.FromMinutes(1));
            hierarchyOperations.Upsert(activities, userActivity2, TimeSpan.FromMinutes(1));

            // Assert
            activities.Count.ShouldBe(2);
            activities.First().Activity.ShouldBe("Opera");
            activities.First().Time.ShouldBe(TimeSpan.FromMinutes(1));
            activities.First().InnerActivities.First().Activity.ShouldBe("YouTube");
            activities.First().InnerActivities.First().Time.ShouldBe(TimeSpan.FromMinutes(1));
            activities.First().InnerActivities.First().InnerActivities.First().Activity.ShouldBe("Nick Chapsas");
            activities.First().InnerActivities.First().InnerActivities.First().Time.ShouldBe(TimeSpan.FromMinutes(1));
            activities.Last().Activity.ShouldBe("WhiteStarUML");
            activities.Last().Time.ShouldBe(TimeSpan.FromMinutes(1));
            activities.Last().InnerActivities.First().Activity.ShouldBe("FocusForge.uml");
            activities.Last().InnerActivities.First().Time.ShouldBe(TimeSpan.FromMinutes(1));
            activities.Last().InnerActivities.First().InnerActivities.Count.ShouldBe(0);
        }

        [Fact]
        public void UpsertActivity_WithExistingEntryInHierarchy_AddTimeToExistingEntry()
        {
            // Arrange
            var hierarchyOperations = new ProcessHierarchyOperations();
            var userActivity = new UserActivity("opera.exe", "Opera", "YouTube", "Some Channel");

            // Act
            hierarchyOperations.Upsert(_testActivities, userActivity, TimeSpan.FromMinutes(1));

            // Assert
            _testActivities.Count.ShouldBe(1);
            _testActivities.First().Activity.ShouldBe("Opera");
            _testActivities.First().Time.ShouldBe(TimeSpan.FromMinutes(6));
            _testActivities.First().InnerActivities.First().Activity.ShouldBe("YouTube");
            _testActivities.First().InnerActivities.First().Time.ShouldBe(TimeSpan.FromMinutes(6));
            _testActivities.First().InnerActivities.First().InnerActivities.First().Activity.ShouldBe("Some Channel");
            _testActivities.First().InnerActivities.First().InnerActivities.First().Time.ShouldBe(TimeSpan.FromMinutes(6));
        }
    }
}
