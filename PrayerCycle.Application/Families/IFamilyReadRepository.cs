using PrayerCycle.Domain.Families;
using PrayerCycle.Domain.Users;

namespace PrayerCycle.Application.Families;

public interface IFamilyReadRepository
{
    Task<Family?> GetByIdAsync(FamilyId id, CancellationToken cancellationToken = default);

    Task<Family?> GetByInviteCodeAsync(InviteCode inviteCode, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Family>> GetByOwnerUserIdAsync(UserId ownerUserId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Family>> GetAllAsync(CancellationToken cancellationToken = default);
}
