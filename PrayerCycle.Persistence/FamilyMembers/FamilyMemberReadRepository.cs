using Microsoft.EntityFrameworkCore;
using PrayerCycle.Application.FamilyMembers;
using PrayerCycle.Domain.Families;
using PrayerCycle.Domain.FamilyMembers;
using PrayerCycle.Domain.Users;
using PrayerCycle.Persistence.Context;

namespace PrayerCycle.Persistence.FamilyMembers;

internal sealed class FamilyMemberReadRepository : IFamilyMemberReadRepository
{
    private readonly PrayerCycleDbContext _context;

    public FamilyMemberReadRepository(PrayerCycleDbContext context)
    {
        _context = context;
    }

    public async Task<FamilyMember?> GetByIdAsync(FamilyMemberId id, CancellationToken cancellationToken = default) =>
        await _context.FamilyMembers.AsNoTracking().FirstOrDefaultAsync(member => member.Id == id, cancellationToken);

    public async Task<IReadOnlyList<FamilyMember>> GetByFamilyIdAsync(
        FamilyId familyId,
        CancellationToken cancellationToken = default) =>
        await _context.FamilyMembers
            .AsNoTracking()
            .Where(member => member.FamilyId == familyId)
            .ToListAsync(cancellationToken);

    public async Task<FamilyMember?> GetByUserIdAsync(UserId userId, CancellationToken cancellationToken = default) =>
        await _context.FamilyMembers.AsNoTracking().FirstOrDefaultAsync(member => member.UserId == userId, cancellationToken);
}
