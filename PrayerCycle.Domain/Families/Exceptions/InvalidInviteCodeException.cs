using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.Families;

/// <summary>
/// Geçersiz davet kodu nedeniyle fırlatılan domain exception.
/// </summary>
public sealed class InvalidInviteCodeException : DomainException
{
    /// <summary>
    /// Belirtilen mesajla yeni bir <see cref="InvalidInviteCodeException"/> oluşturur.
    /// </summary>
    /// <param name="message">Hata mesajı.</param>
    public InvalidInviteCodeException(string message)
        : base(message)
    {
    }
}
