using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.Users;

/// <summary>
/// Yeni bir kullanıcının başarıyla kaydedildiğini bildiren domain event.
/// </summary>
/// <param name="UserId">Kaydedilen kullanıcının kimliği.</param>
/// <param name="Email">Kaydedilen kullanıcının e-posta adresi.</param>
public sealed record UserRegistered(UserId UserId, Email Email) : DomainEvent;
