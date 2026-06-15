using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.PrayerLogs;

/// <summary>
/// Geçersiz namaz notu nedeniyle fırlatılan domain exception.
/// </summary>
public sealed class InvalidPrayerNotesException : DomainException
{
    /// <summary>
    /// Belirtilen mesajla yeni bir <see cref="InvalidPrayerNotesException"/> oluşturur.
    /// </summary>
    /// <param name="message">Hata mesajı.</param>
    public InvalidPrayerNotesException(string message)
        : base(message)
    {
    }
}
