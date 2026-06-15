using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.Families;

/// <summary>
/// Geçersiz aile adı nedeniyle fırlatılan domain exception.
/// </summary>
public sealed class InvalidFamilyNameException : DomainException
{
    /// <summary>
    /// Belirtilen mesajla yeni bir <see cref="InvalidFamilyNameException"/> oluşturur.
    /// </summary>
    /// <param name="message">Hata mesajı.</param>
    public InvalidFamilyNameException(string message)
        : base(message)
    {
    }
}
