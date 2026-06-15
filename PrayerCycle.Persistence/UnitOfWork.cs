using PrayerCycle.Application.Common.Abstractions;
using PrayerCycle.Persistence.Context;

namespace PrayerCycle.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly PrayerCycleDbContext _context;

    public UnitOfWork(PrayerCycleDbContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _context.SaveChangesAsync(cancellationToken);
}
