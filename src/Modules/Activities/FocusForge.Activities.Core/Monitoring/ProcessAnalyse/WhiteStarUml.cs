using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.Activities.Core.Monitoring.ProcessAnalyse
{
    internal class WhiteStarUml : IProcessDescription
    {
        public string Name => "WhiteStarUML.exe";

        public UserActivity Describe(ActiveProcessInfo processInfo)
        {
            var titleElements = processInfo.WindowTitle.Split('-', StringSplitOptions.TrimEntries & StringSplitOptions.TrimEntries);
            return new UserActivity(processInfo.ExecutableName, titleElements[0], titleElements[1], string.Empty);
        }
    }
}
