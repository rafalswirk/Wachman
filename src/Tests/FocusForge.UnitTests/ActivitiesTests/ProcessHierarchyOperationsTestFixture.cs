using FocusForge.Activities.Core.Monitoring;
using FocusForge.Activities.Core.Monitoring.ProcessAnalyse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FocusForge.UnitTests.ActivitiesTests
{
    public class ProcessHierarchyOperationsTestFixture
    {
        [Fact]
        public void Convert_ValidProcessInfo_ReturnsActivity()
        {
            // Arrange
            var converter = new ProcessInfoToActivityConverter();
            converter.RegisterProcessDescription(new NotepadDescription());
            var processInfo = new ActiveProcessInfo("notepad.exe", "*Wielki Gatsby - Notepad");
            // Act
            var activity = converter.Convert(processInfo);
            // Assert
            Assert.NotNull(activity);
            Assert.Equal("notepad.exe", activity.Executable);
            Assert.Equal("Notepad", activity.ApplicationName);
            Assert.Equal("Wielki Gatsby", activity.ActivityDescription);
        }

        [Theory]
        [MemberData(nameof(GetProcessInfos))]
        public void Convert_UnknownProcessInfo_ReturnsGenericActivity(ActiveProcessInfo inputProcessInfo, UserActivity expectedActivity)
        {
            // Arrange
            var converter = new ProcessInfoToActivityConverter();
            // Act
            var activity = converter.Convert(inputProcessInfo);
            // Assert
            Assert.NotNull(activity);
            Assert.Equal(expectedActivity.Executable, activity.Executable);
            Assert.Equal(expectedActivity.ApplicationName, activity.ApplicationName);
            Assert.Equal(expectedActivity.ActivityDescription, activity.ActivityDescription);
        }

        public static IEnumerable<object[]> GetProcessInfos()
        {
            yield return new object[] { new ActiveProcessInfo("notepad.exe", "*Wielki Gatsby Notepad"), 
                new UserActivity("notepad.exe", "Wielki Gatsby Notepad", string.Empty, string.Empty)};

            yield return new object[] { new ActiveProcessInfo("notepad.exe", "*Wielki Gatsby - Notepad"),
                new UserActivity("notepad.exe", "Notepad", "Wielki Gatsby", string.Empty)};
        }

        [Theory]
        [MemberData(nameof(GetOperaProcessInfos))]
        public void Convert_OperaProcessInfo_ReturnsUserActivity(ActiveProcessInfo inputProcessInfo, UserActivity expectedActivity)
        {
            // Arrange
            var converter = new ProcessInfoToActivityConverter();
            converter.RegisterProcessDescription(new OperaProcessDescription());
            // Act
            var activity = converter.Convert(inputProcessInfo);
            // Assert
            Assert.NotNull(activity);
            Assert.Equal(expectedActivity.Executable, activity.Executable);
            Assert.Equal(expectedActivity.ApplicationName, activity.ApplicationName);
            Assert.Equal(expectedActivity.ActivityDescription, activity.ActivityDescription);
        }

        public static IEnumerable<object[]> GetOperaProcessInfos()
        {
            yield return new object[] { new ActiveProcessInfo("opera.exe", "Poczta — Joe Doe — Outlook - Opera"),
                new UserActivity(Executable: "opera.exe", ApplicationName: "Opera", ActivityDescription: "Outlook", InnerActivityDescription: "Poczta — Joe Doe")};

            yield return new object[] { new ActiveProcessInfo("opera.exe", "Sign in to GitHub · GitHub - Opera"),
                new UserActivity("opera.exe", "Opera", "GitHub", "Sign in to GitHub")};

            yield return new object[] { new ActiveProcessInfo("opera.exe", "Google - Opera"),
                new UserActivity("opera.exe", "Opera", "Google", string.Empty)};

            yield return new object[] { new ActiveProcessInfo("opera.exe", "Nick Chapsas - YouTube - Opera"),
                new UserActivity("opera.exe", "Opera", "YouTube", "Nick Chapsas")};

            yield return new object[] { new ActiveProcessInfo("opera.exe", "Sending Email Correctly in .NET - YouTube - Opera"),
                new UserActivity("opera.exe", "Opera", "YouTube", "Sending Email Correctly in .NET")};

            yield return new object[] { new ActiveProcessInfo("opera.exe", "00:00:13 | TimeCamp - Opera"),
                new UserActivity("opera.exe", "Opera", "TimeCamp", "00:00:13")};
        }
    }
}
