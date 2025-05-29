using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.TimeCampAPI.DTO
{
    public record TimerStatusDTO(bool IsTimerRunning, long Elapsed);
}
