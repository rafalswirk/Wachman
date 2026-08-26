using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.Activities.Core.Monitoring.ProcessAnalyse
{
    public class MonitoredActivity
    {
        public UserActivity Activity { get; set; }
        public long Ticks { get; set; }
    }
}
