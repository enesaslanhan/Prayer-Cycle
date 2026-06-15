using Microsoft.EntityFrameworkCore;
using PrayerCycle.Application.PrayerLogs;
using PrayerCycle.Domain.FamilyMembers;
using PrayerCycle.Domain.PrayerLogs;
using PrayerCycle.Persistence.Context;

namespace PrayerCycle.Persistence.PrayerLogs;

internal sealed class PrayerLogReadRepository : IPrayerLogReadRepository
{
    private readonly PrayerCycleDbContext _context;

    public PrayerLogReadRepository(PrayerCycleDbContext context)
    {
        _context = context;
    }

    public async Task<PrayerLog?> GetByIdAsync(PrayerLogId id, CancellationToken cancellationToken = default) =>
        await _context.PrayerLogs.AsNoTracking().FirstOrDefaultAsync(prayerLog => prayerLog.Id == id, cancellationToken);

    public async Task<IReadOnlyList<PrayerLog>> GetByFamilyMemberIdAsync(
        FamilyMemberId familyMemberId,
        CancellationToken cancellationToken = default) =>
        await _context.PrayerLogs
            .AsNoTracking()
            .Where(prayerLog => prayerLog.FamilyMemberId == familyMemberId)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<PrayerLog>> GetByFamilyMemberIdAndDateAsync(
        FamilyMemberId familyMemberId,
        DateOnly date,
        CancellationToken cancellationToken = default) =>
        await _context.PrayerLogs
            .AsNoTracking()
            .Where(prayerLog => prayerLog.FamilyMemberId == familyMemberId && prayerLog.Date == date)
            .ToListAsync(cancellationToken);
}
