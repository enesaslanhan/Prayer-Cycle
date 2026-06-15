using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.FamilyMembers;

/// <summary>
/// Pasif aile üyesi üzerinde izin verilmeyen bir işlem denendiğinde fırlatılan domain exception.
/// </summary>
public sealed class FamilyMemberInactiveException : DomainException
{
    /// <summary>
    /// Varsayılan mesajla yeni bir <see cref="FamilyMemberInactiveException"/> oluşturur.
    /// </summary>
    public FamilyMemberInactiveException()
        : base("Inactive family members cannot perform this action.")
    {
    }
}
