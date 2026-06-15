using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.FamilyMembers;

/// <summary>
/// Geçersiz üye adı nedeniyle fırlatılan domain exception.
/// </summary>
public sealed class InvalidMemberNameException : DomainException
{
    /// <summary>
    /// Belirtilen mesajla yeni bir <see cref="InvalidMemberNameException"/> oluşturur.
    /// </summary>
    /// <param name="message">Hata mesajı.</param>
    public InvalidMemberNameException(string message)
        : base(message)
    {
    }
}
