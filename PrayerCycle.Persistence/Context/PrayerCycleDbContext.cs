using Microsoft.EntityFrameworkCore;
using PrayerCycle.Domain.Families;
using PrayerCycle.Domain.FamilyMembers;
using PrayerCycle.Domain.PrayerLogs;
using PrayerCycle.Domain.Users;

namespace PrayerCycle.Persistence.Context;

public sealed class PrayerCycleDbContext : DbContext
{
    public PrayerCycleDbContext(DbContextOptions<PrayerCycleDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Family> Families => Set<Family>();

    public DbSet<FamilyMember> FamilyMembers => Set<FamilyMember>();

    public DbSet<PrayerLog> PrayerLogs => Set<PrayerLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PrayerCycleDbContext).Assembly);
    }
}
