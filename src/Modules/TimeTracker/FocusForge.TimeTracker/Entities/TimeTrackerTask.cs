using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.Entities
{
    public class TimeTrackerTask
    {
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public string Name { get; set; }
    }
}
