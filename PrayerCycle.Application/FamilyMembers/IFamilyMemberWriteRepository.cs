using PrayerCycle.Domain.FamilyMembers;

namespace PrayerCycle.Application.FamilyMembers;

public interface IFamilyMemberWriteRepository
{
    Task AddAsync(FamilyMember familyMember, CancellationToken cancellationToken = default);

    Task UpdateAsync(FamilyMember familyMember, CancellationToken cancellationToken = default);

    Task RemoveAsync(FamilyMember familyMember, CancellationToken cancellationToken = default);
}
