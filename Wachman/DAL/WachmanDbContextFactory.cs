using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wachman.DAL
{
    public class WachmanDbContextFactory : IDesignTimeDbContextFactory<WachmanDbContext>
    {
        public WachmanDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WachmanDbContext>();
            optionsBuilder.UseSqlite("Data Source=Wachman.db");

            return new WachmanDbContext(optionsBuilder.Options);

        }
    }
}
