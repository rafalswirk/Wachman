using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.PomodoroTimer.CustomEventArgs
{
    public class OnSessionFinishedEventArgs : EventArgs
    {
        public bool InterruptedByUser { get; set; }
    }
}
