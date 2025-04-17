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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AppSetting>().HasData(
            new AppSetting { Id = 1, SettingsKey = "WorkSessionDuration", SettingsValue = "30" },
            new AppSetting { Id = 2, SettingsKey = "BreakTimeDuration", SettingsValue = "5" },
            new AppSetting { Id = 3, SettingsKey = "DisableBreaks", SettingsValue = "0" }
        );
    }
}
