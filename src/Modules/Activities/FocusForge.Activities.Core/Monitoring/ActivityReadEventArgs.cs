using FocusForge.Activities.Core.Monitoring.ProcessAnalyse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.Activities.Core.Monitoring
{
    public class ActivityReadEventArgs: EventArgs
    {
        public UserActivity Activity { get; set; }
        public TimeSpan Time { get; set; }
    }
}
