using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.FamilyMembers;

/// <summary>
/// Üye rolü ile ilişkili iş kuralı ihlallerinde fırlatılan domain exception.
/// </summary>
public sealed class InvalidMemberRoleException : DomainException
{
    /// <summary>
    /// Belirtilen mesajla yeni bir <see cref="InvalidMemberRoleException"/> oluşturur.
    /// </summary>
    /// <param name="message">Hata mesajı.</param>
    public InvalidMemberRoleException(string message)
        : base(message)
    {
    }
}
