using System;
using Microsoft.EntityFrameworkCore;
using Wachman.Entities;

namespace Wachman.DAL;

public class WachmanDbContext : DbContext
{
    public DbSet<AppSetting> Settings { get; set; } = null!;

    public WachmanDbContext() {}
    public WachmanDbContext(DbContextOptions<WachmanDbContext> options)
        : base(options)
    {
            
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Wachman.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AppSetting>().HasData(SeedDataGenerator.Generate());
    }
}
