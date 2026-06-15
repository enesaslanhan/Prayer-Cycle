namespace PrayerCycle.Domain.Common;

/// <summary>
/// Domain katmanında ihlal edilen iş kurallarını temsil eden temel exception.
/// </summary>
public class DomainException : Exception
{
    /// <summary>
    /// Belirtilen mesajla yeni bir domain exception oluşturur.
    /// </summary>
    /// <param name="message">Hata mesajı.</param>
    public DomainException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Belirtilen mesaj ve iç exception ile yeni bir domain exception oluşturur.
    /// </summary>
    /// <param name="message">Hata mesajı.</param>
    /// <param name="innerException">İç exception.</param>
    public DomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
