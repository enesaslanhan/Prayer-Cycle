using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.FamilyMembers;

/// <summary>
/// Geçersiz avatar rengi nedeniyle fırlatılan domain exception.
/// </summary>
public sealed class InvalidAvatarColorException : DomainException
{
    /// <summary>
    /// Belirtilen mesajla yeni bir <see cref="InvalidAvatarColorException"/> oluşturur.
    /// </summary>
    /// <param name="message">Hata mesajı.</param>
    public InvalidAvatarColorException(string message)
        : base(message)
    {
    }
}
