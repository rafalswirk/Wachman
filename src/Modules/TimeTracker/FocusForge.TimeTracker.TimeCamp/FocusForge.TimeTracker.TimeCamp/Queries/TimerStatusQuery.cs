using FocusForge.Shared.Abstractions.Queries;
using FocusForge.TimeTracker.TimeCamp.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.TimeCamp.Queries
{
    public record TimerStatusQuery: IQuery<TimerStatusDTO>;
}
