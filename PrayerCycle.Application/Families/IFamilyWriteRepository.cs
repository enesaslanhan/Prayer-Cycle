using PrayerCycle.Domain.Families;

namespace PrayerCycle.Application.Families;

public interface IFamilyWriteRepository
{
    Task AddAsync(Family family, CancellationToken cancellationToken = default);

    Task UpdateAsync(Family family, CancellationToken cancellationToken = default);

    Task RemoveAsync(Family family, CancellationToken cancellationToken = default);
}
