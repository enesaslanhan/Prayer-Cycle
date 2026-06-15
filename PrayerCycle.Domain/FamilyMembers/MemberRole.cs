namespace PrayerCycle.Domain.FamilyMembers;

/// <summary>
/// Aile üyesinin aile içindeki rolünü tanımlar.
/// </summary>
public enum MemberRole
{
    /// <summary>
    /// Yetişkin ebeveyn profili.
    /// </summary>
    Parent = 1,

    /// <summary>
    /// Çocuk profili.
    /// </summary>
    Child = 2
}
