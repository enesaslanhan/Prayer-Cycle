using PrayerCycle.Domain.Users;

namespace PrayerCycle.Application.Users;

public interface IUserWriteRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken = default);

    Task UpdateAsync(User user, CancellationToken cancellationToken = default);

    Task RemoveAsync(User user, CancellationToken cancellationToken = default);
}
