namespace PrayerCycle.Domain.PrayerLogs;

/// <summary>
/// Namaz kaydının durumunu tanımlar.
/// </summary>
public enum PrayerStatus
{
    /// <summary>
    /// Namaz kılınmış.
    /// </summary>
    Performed = 1,

    /// <summary>
    /// Namaz kaçırılmış.
    /// </summary>
    Missed = 2
}
