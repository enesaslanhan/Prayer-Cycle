using PrayerCycle.Domain.Families;
using PrayerCycle.Domain.FamilyMembers;
using PrayerCycle.Domain.Users;

namespace PrayerCycle.Application.FamilyMembers;

public interface IFamilyMemberReadRepository
{
    Task<FamilyMember?> GetByIdAsync(FamilyMemberId id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<FamilyMember>> GetByFamilyIdAsync(FamilyId familyId, CancellationToken cancellationToken = default);

    Task<FamilyMember?> GetByUserIdAsync(UserId userId, CancellationToken cancellationToken = default);
}
