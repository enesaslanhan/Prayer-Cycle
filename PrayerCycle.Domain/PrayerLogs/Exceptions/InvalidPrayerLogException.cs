using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.PrayerLogs;

/// <summary>
/// Namaz kaydı iş kuralları ihlal edildiğinde fırlatılan domain exception.
/// </summary>
public sealed class InvalidPrayerLogException : DomainException
{
    /// <summary>
    /// Belirtilen mesajla yeni bir <see cref="InvalidPrayerLogException"/> oluşturur.
    /// </summary>
    /// <param name="message">Hata mesajı.</param>
    public InvalidPrayerLogException(string message)
        : base(message)
    {
    }
}
