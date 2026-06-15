using Microsoft.EntityFrameworkCore;
using PrayerCycle.Application.Users;
using PrayerCycle.Domain.Users;
using PrayerCycle.Persistence.Context;

namespace PrayerCycle.Persistence.Users;

internal sealed class UserReadRepository : IUserReadRepository
{
    private readonly PrayerCycleDbContext _context;

    public UserReadRepository(PrayerCycleDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default) =>
        await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Id == id, cancellationToken);

    public async Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default) =>
        await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email == email, cancellationToken);

    public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _context.Users.AsNoTracking().OrderBy(user => user.CreatedAt).ToListAsync(cancellationToken);
}
