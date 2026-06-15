using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.FamilyMembers;

/// <summary>
/// Geçersiz doğum tarihi nedeniyle fırlatılan domain exception.
/// </summary>
public sealed class InvalidBirthDateException : DomainException
{
    /// <summary>
    /// Belirtilen mesajla yeni bir <see cref="InvalidBirthDateException"/> oluşturur.
    /// </summary>
    /// <param name="message">Hata mesajı.</param>
    public InvalidBirthDateException(string message)
        : base(message)
    {
    }
}
