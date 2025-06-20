using FocusForge.DataModels.Entities;
using FocusForge.TimeTracker.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.TimeTracker.DAL
{
    public class TimeTrackerDbContext : DbContext
    {
        public DbSet<TimeTrackerTask> TimeTrackerTasks { get; internal set; }
        public DbSet<AppSetting> Settings { get; set; } = null!;
        
        public TimeTrackerDbContext() {}

        public TimeTrackerDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionStringProvider.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
