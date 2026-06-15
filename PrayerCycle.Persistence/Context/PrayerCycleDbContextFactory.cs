using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PrayerCycle.Persistence.Context;

internal sealed class PrayerCycleDbContextFactory : IDesignTimeDbContextFactory<PrayerCycleDbContext>
{
    public PrayerCycleDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PrayerCycleDbContext>();
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\mssqllocaldb;Database=PrayerCycle;Trusted_Connection=True;TrustServerCertificate=True");

        return new PrayerCycleDbContext(optionsBuilder.Options);
    }
}
