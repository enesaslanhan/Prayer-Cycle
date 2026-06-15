using PrayerCycle.Application.PrayerLogs;
using PrayerCycle.Domain.PrayerLogs;
using PrayerCycle.Persistence.Context;

namespace PrayerCycle.Persistence.PrayerLogs;

internal sealed class PrayerLogWriteRepository : IPrayerLogWriteRepository
{
    private readonly PrayerCycleDbContext _context;

    public PrayerLogWriteRepository(PrayerCycleDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(PrayerLog prayerLog, CancellationToken cancellationToken = default) =>
        await _context.PrayerLogs.AddAsync(prayerLog, cancellationToken);

    public Task UpdateAsync(PrayerLog prayerLog, CancellationToken cancellationToken = default)
    {
        _context.PrayerLogs.Update(prayerLog);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(PrayerLog prayerLog, CancellationToken cancellationToken = default)
    {
        _context.PrayerLogs.Remove(prayerLog);
        return Task.CompletedTask;
    }
}
