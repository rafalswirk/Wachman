using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.PomodoroTimer.DAL
{
    public class WachmanDbContextFactory : IDesignTimeDbContextFactory<WachmanDbContext>
    {
        public WachmanDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WachmanDbContext>();
            optionsBuilder.UseSqlite(ConnectionStringProvider.ConnectionString);

            return new WachmanDbContext(optionsBuilder.Options);

        }
    }
}
