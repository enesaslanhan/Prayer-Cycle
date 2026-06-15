using PrayerCycle.Application.Users;
using PrayerCycle.Domain.Users;
using PrayerCycle.Persistence.Context;

namespace PrayerCycle.Persistence.Users;

internal sealed class UserWriteRepository : IUserWriteRepository
{
    private readonly PrayerCycleDbContext _context;

    public UserWriteRepository(PrayerCycleDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default) =>
        await _context.Users.AddAsync(user, cancellationToken);

    public Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        _context.Users.Update(user);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(User user, CancellationToken cancellationToken = default)
    {
        _context.Users.Remove(user);
        return Task.CompletedTask;
    }
}
