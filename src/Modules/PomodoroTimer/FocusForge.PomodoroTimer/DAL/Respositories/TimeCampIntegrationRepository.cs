using FocusForge.PomodoroTimer.Models;
using FocusForge.PomodoroTimer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.PomodoroTimer.DAL.Respositories
{
    internal class TimeCampIntegrationRepository : ITimeCampIntegrationRepository
    {
        private readonly WachmanDbContext _context;

        public TimeCampIntegrationRepository(WachmanDbContext context)
        {
            _context = context;
        }
        public TimeCampIntegrationData TimeCampApiKey
            => throw new NotImplementedException();

        public void SaveTimeCampIntegrationData(TimeCampIntegrationData timeCampApiKey)
        {
            throw new NotImplementedException();
        }
    }
}
