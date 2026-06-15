using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.Users;

/// <summary>
/// Pasif kullanıcı üzerinde izin verilmeyen bir işlem denendiğinde fırlatılan domain exception.
/// </summary>
public sealed class UserInactiveException : DomainException
{
    /// <summary>
    /// Varsayılan mesajla yeni bir <see cref="UserInactiveException"/> oluşturur.
    /// </summary>
    public UserInactiveException()
        : base("Inactive users cannot perform this action.")
    {
    }
}
