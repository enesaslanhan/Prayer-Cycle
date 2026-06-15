namespace PrayerCycle.Domain.PrayerLogs;

/// <summary>
/// Namaz vakitlerini tanımlar.
/// </summary>
public enum PrayerTime
{
    /// <summary>
    /// Sabah namazı.
    /// </summary>
    Fajr = 1,

    /// <summary>
    /// Öğle namazı.
    /// </summary>
    Dhuhr = 2,

    /// <summary>
    /// İkindi namazı.
    /// </summary>
    Asr = 3,

    /// <summary>
    /// Akşam namazı.
    /// </summary>
    Maghrib = 4,

    /// <summary>
    /// Yatsı namazı.
    /// </summary>
    Isha = 5
}
