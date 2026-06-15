namespace PrayerCycle.Domain.PrayerLogs;

/// <summary>
/// Kılınan namazın türünü tanımlar.
/// </summary>
public enum PrayerType
{
    /// <summary>
    /// Tek başına kılınan namaz.
    /// </summary>
    Individual = 1,

    /// <summary>
    /// Cemaatle kılınan namaz.
    /// </summary>
    Congregation = 2
}
