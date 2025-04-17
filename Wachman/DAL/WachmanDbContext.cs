using System;
using Microsoft.EntityFrameworkCore;
using Wachman.Entities;

namespace Wachman.DAL;

public class WachmanDbContext : DbContext
{
    public DbSet<AppSetting> Settings { get; set; } = null!;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Wachman.db");
    }
}
