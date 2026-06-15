using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.Users;

/// <summary>
/// Geçersiz e-posta adresi nedeniyle fırlatılan domain exception.
/// </summary>
public sealed class InvalidEmailException : DomainException
{
    /// <summary>
    /// Belirtilen mesajla yeni bir <see cref="InvalidEmailException"/> oluşturur.
    /// </summary>
    /// <param name="message">Hata mesajı.</param>
    public InvalidEmailException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Belirtilen mesaj ve iç exception ile yeni bir <see cref="InvalidEmailException"/> oluşturur.
    /// </summary>
    /// <param name="message">Hata mesajı.</param>
    /// <param name="innerException">İç exception.</param>
    public InvalidEmailException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
