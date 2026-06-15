using PrayerCycle.Application.Families;
using PrayerCycle.Domain.Families;
using PrayerCycle.Persistence.Context;

namespace PrayerCycle.Persistence.Families;

internal sealed class FamilyWriteRepository : IFamilyWriteRepository
{
    private readonly PrayerCycleDbContext _context;

    public FamilyWriteRepository(PrayerCycleDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Family family, CancellationToken cancellationToken = default) =>
        await _context.Families.AddAsync(family, cancellationToken);

    public Task UpdateAsync(Family family, CancellationToken cancellationToken = default)
    {
        _context.Families.Update(family);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(Family family, CancellationToken cancellationToken = default)
    {
        _context.Families.Remove(family);
        return Task.CompletedTask;
    }
}
