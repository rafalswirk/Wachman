using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wachman.DAL
{
    internal static class AutomatedMigrations
    {
        public static void Apply()
        {
            using var context = new WachmanDbContext();
            context.Database.Migrate();
        }
    }
}
