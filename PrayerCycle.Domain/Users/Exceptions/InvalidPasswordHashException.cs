using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.Users;

/// <summary>
/// Geçersiz şifre hash değeri nedeniyle fırlatılan domain exception.
/// </summary>
public sealed class InvalidPasswordHashException : DomainException
{
    /// <summary>
    /// Belirtilen mesajla yeni bir <see cref="InvalidPasswordHashException"/> oluşturur.
    /// </summary>
    /// <param name="message">Hata mesajı.</param>
    public InvalidPasswordHashException(string message)
        : base(message)
    {
    }
}
