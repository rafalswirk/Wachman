using FocusForge.Shared.DataModels.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.TimeCamp.DAL.DTO
{
    public record DailyJobsDTO(List<Job> Jobs);
}
