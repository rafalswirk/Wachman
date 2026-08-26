using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.Activities.Core.Monitoring.ProcessAnalyse
{
    public class NotepadDescription : IProcessDescription
    {
        public string Name => "notepad.exe";

        public UserActivity Describe(ActiveProcessInfo processInfo)
        {
            var windowTitle = processInfo.WindowTitle.Split('-', StringSplitOptions.RemoveEmptyEntries).Select(part => part.Trim()).ToArray();
            var userActivity = new UserActivity(processInfo.ExecutableName, windowTitle.Last(), windowTitle[windowTitle.Length - 2].Replace("*", ""), string.Empty);
            return userActivity;
        }
    }
}
