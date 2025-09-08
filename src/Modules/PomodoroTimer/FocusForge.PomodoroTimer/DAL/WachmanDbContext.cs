using System;
using FocusForge.Shared.DataModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace FocusForge.PomodoroTimer.DAL;

public class WachmanDbContext : DbContext
{
    public DbSet<AppSetting> Settings { get; set; } = null!;

    public WachmanDbContext() { }
    public WachmanDbContext(DbContextOptions<WachmanDbContext> options)
        : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(ConnectionStringProvider.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AppSetting>().HasData(SeedDataGenerator.Generate());
    }
}
