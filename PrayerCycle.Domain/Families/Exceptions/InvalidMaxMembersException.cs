using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.Families;

/// <summary>
/// Geçersiz maksimum üye sayısı nedeniyle fırlatılan domain exception.
/// </summary>
public sealed class InvalidMaxMembersException : DomainException
{
    /// <summary>
    /// Belirtilen mesajla yeni bir <see cref="InvalidMaxMembersException"/> oluşturur.
    /// </summary>
    /// <param name="message">Hata mesajı.</param>
    public InvalidMaxMembersException(string message)
        : base(message)
    {
    }
}
