using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.Users;

/// <summary>
/// Bir kullanıcının giriş yaptığını bildiren domain event.
/// </summary>
/// <param name="UserId">Giriş yapan kullanıcının kimliği.</param>
/// <param name="LoginAt">Giriş zamanı (UTC).</param>
public sealed record UserLoggedIn(UserId UserId, DateTime LoginAt) : DomainEvent;
