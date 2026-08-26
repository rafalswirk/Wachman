using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.Activities.Core.Monitoring.ProcessAnalyse
{
    public record UserActivity(string Executable, string ApplicationName, string ActivityDescription, string InnerActivityDescription);
}
