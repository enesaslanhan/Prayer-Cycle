using PrayerCycle.Domain.FamilyMembers;
using PrayerCycle.Domain.PrayerLogs;

namespace PrayerCycle.Application.PrayerLogs;

public interface IPrayerLogReadRepository
{
    Task<PrayerLog?> GetByIdAsync(PrayerLogId id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PrayerLog>> GetByFamilyMemberIdAsync(
        FamilyMemberId familyMemberId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PrayerLog>> GetByFamilyMemberIdAndDateAsync(
        FamilyMemberId familyMemberId,
        DateOnly date,
        CancellationToken cancellationToken = default);
}
