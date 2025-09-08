using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.Entities
{
    public class TimeTrackerTask
    {
        public long Id { get; set; }
        public long ExternalId { get; set; }
        public string Name { get; set; }
    }
}
