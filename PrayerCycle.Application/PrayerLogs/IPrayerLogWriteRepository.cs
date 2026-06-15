using PrayerCycle.Domain.PrayerLogs;

namespace PrayerCycle.Application.PrayerLogs;

public interface IPrayerLogWriteRepository
{
    Task AddAsync(PrayerLog prayerLog, CancellationToken cancellationToken = default);

    Task UpdateAsync(PrayerLog prayerLog, CancellationToken cancellationToken = default);

    Task RemoveAsync(PrayerLog prayerLog, CancellationToken cancellationToken = default);
}
