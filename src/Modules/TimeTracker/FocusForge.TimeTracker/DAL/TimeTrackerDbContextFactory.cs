using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.DAL
{
    public class TimeTrackerDbContextFactory : IDesignTimeDbContextFactory<TimeTrackerDbContext>
    {           
        public TimeTrackerDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TimeTrackerDbContext>();
            builder.UseSqlite(ConnectionStringProvider.ConnectionString);

            return new TimeTrackerDbContext(builder.Options);
        }
    }
}
