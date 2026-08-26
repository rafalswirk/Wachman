namespace FocusForge.Activities.Core.Monitoring.ProcessAnalyse
{
    internal class DefaultActivityDescription : IProcessDescription
    {
        public string Name => nameof(DefaultActivityDescription);

        public UserActivity Describe(ActiveProcessInfo processInfo)
        {
            if(processInfo.WindowTitle.Contains("-"))
            {
                var windowTitleParts = processInfo.WindowTitle.Split(["-"], StringSplitOptions.RemoveEmptyEntries);
                return new UserActivity(processInfo.ExecutableName, windowTitleParts.Last().Trim(), windowTitleParts[windowTitleParts.Length - 2].Replace("*", "").Trim(), string.Empty);
            }
            return new UserActivity(processInfo.ExecutableName, processInfo.WindowTitle.Replace("*", "").Trim(), string.Empty, string.Empty);
        }
    }
}