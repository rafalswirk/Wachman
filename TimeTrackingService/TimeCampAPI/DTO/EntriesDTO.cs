using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTrackingService.TimeCampAPI.DTO
{
    internal class EntriesDTO
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string Start_Time { get; set; }
        public string End_Time { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}
