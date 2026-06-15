using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.FamilyMembers;

/// <summary>
/// Geçersiz avatar baş harfi nedeniyle fırlatılan domain exception.
/// </summary>
public sealed class InvalidAvatarInitialException : DomainException
{
    /// <summary>
    /// Belirtilen mesajla yeni bir <see cref="InvalidAvatarInitialException"/> oluşturur.
    /// </summary>
    /// <param name="message">Hata mesajı.</param>
    public InvalidAvatarInitialException(string message)
        : base(message)
    {
    }
}
