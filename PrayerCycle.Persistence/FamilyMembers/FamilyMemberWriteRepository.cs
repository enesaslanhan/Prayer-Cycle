using PrayerCycle.Application.FamilyMembers;
using PrayerCycle.Domain.FamilyMembers;
using PrayerCycle.Persistence.Context;

namespace PrayerCycle.Persistence.FamilyMembers;

internal sealed class FamilyMemberWriteRepository : IFamilyMemberWriteRepository
{
    private readonly PrayerCycleDbContext _context;

    public FamilyMemberWriteRepository(PrayerCycleDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(FamilyMember familyMember, CancellationToken cancellationToken = default) =>
        await _context.FamilyMembers.AddAsync(familyMember, cancellationToken);

    public Task UpdateAsync(FamilyMember familyMember, CancellationToken cancellationToken = default)
    {
        _context.FamilyMembers.Update(familyMember);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(FamilyMember familyMember, CancellationToken cancellationToken = default)
    {
        _context.FamilyMembers.Remove(familyMember);
        return Task.CompletedTask;
    }
}
