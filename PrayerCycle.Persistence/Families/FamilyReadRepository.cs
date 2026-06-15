using Microsoft.EntityFrameworkCore;
using PrayerCycle.Application.Families;
using PrayerCycle.Domain.Families;
using PrayerCycle.Domain.Users;
using PrayerCycle.Persistence.Context;

namespace PrayerCycle.Persistence.Families;

internal sealed class FamilyReadRepository : IFamilyReadRepository
{
    private readonly PrayerCycleDbContext _context;

    public FamilyReadRepository(PrayerCycleDbContext context)
    {
        _context = context;
    }

    public async Task<Family?> GetByIdAsync(FamilyId id, CancellationToken cancellationToken = default) =>
        await _context.Families.AsNoTracking().FirstOrDefaultAsync(family => family.Id == id, cancellationToken);

    public async Task<Family?> GetByInviteCodeAsync(InviteCode inviteCode, CancellationToken cancellationToken = default) =>
        await _context.Families.AsNoTracking().FirstOrDefaultAsync(family => family.InviteCode == inviteCode, cancellationToken);

    public async Task<IReadOnlyList<Family>> GetByOwnerUserIdAsync(
        UserId ownerUserId,
        CancellationToken cancellationToken = default) =>
        await _context.Families
            .AsNoTracking()
            .Where(family => family.OwnerUserId == ownerUserId)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Family>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _context.Families.AsNoTracking().OrderBy(family => family.CreatedAt).ToListAsync(cancellationToken);
}
