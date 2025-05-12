using FocusForge.DataModels.Entities;
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
        public TimeTrackerDbContext() {}

        public TimeTrackerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppSetting> Settings { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionStringProvider.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
