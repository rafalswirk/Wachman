using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.Activities.Core.Monitoring.ProcessAnalyse
{
    internal class OperaProcessDescription : IProcessDescription
    {
        // List of window separator strings; you may extend this.
        private readonly string[] WindowSeparators = [" - ", " · ", " — ", " | ", " • "];

        public string Name => "opera.exe";

        public UserActivity Describe(ActiveProcessInfo processInfo)
        {
            return Convert(processInfo);
        }

        public UserActivity Convert(ActiveProcessInfo proc)
        {
            var parts = SplitBySeparators(proc.WindowTitle, WindowSeparators)
                .Select(p => p.Trim())
                .Where(p => !string.IsNullOrEmpty(p))
                .ToList();

            string executable = proc.ExecutableName;
            string applicationName = parts.Count > 0 ? parts.Last() : string.Empty;
            string activityDescription = string.Empty;
            string innerActivityDescription = string.Empty;

            if (parts.Count >= 3)
            {
                activityDescription = parts[^2];
                innerActivityDescription = string.Join(" — ", parts.Take(parts.Count - 2));
            }
            else if (parts.Count == 2)
            {
                activityDescription = parts[0];
                innerActivityDescription = string.Empty;
            }
            // If only one part, leave descriptions as empty

            return new UserActivity(
                Executable: executable,
                ApplicationName: applicationName,
                ActivityDescription: activityDescription,
                InnerActivityDescription: innerActivityDescription
            );
        }

        private List<string> SplitBySeparators(string input, string[] separators)
        {
            var result = new List<string> { input };
            foreach (var sep in separators)
            {
                var tempResult = new List<string>();
                foreach (var item in result)
                {
                    tempResult.AddRange(item.Split(sep, StringSplitOptions.None));
                }
                result = tempResult;
            }
            return result;
        }

    }
}
