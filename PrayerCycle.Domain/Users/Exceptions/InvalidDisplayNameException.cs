using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.Users;

/// <summary>
/// Geçersiz görünen ad nedeniyle fırlatılan domain exception.
/// </summary>
public sealed class InvalidDisplayNameException : DomainException
{
    /// <summary>
    /// Belirtilen mesajla yeni bir <see cref="InvalidDisplayNameException"/> oluşturur.
    /// </summary>
    /// <param name="message">Hata mesajı.</param>
    public InvalidDisplayNameException(string message)
        : base(message)
    {
    }
}
