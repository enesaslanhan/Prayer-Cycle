using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.PrayerLogs;

/// <summary>
/// Mevcut bir namaz kaydının güncellendiğini bildiren domain event.
/// </summary>
/// <param name="PrayerLogId">Güncellenen namaz kaydının kimliği.</param>
/// <param name="Status">Güncel namaz durumu.</param>
/// <param name="Type">Güncel namaz türü.</param>
/// <param name="IsQada">Kaza namazı olup olmadığı.</param>
public sealed record PrayerLogUpdated(
    PrayerLogId PrayerLogId,
    PrayerStatus Status,
    PrayerType? Type,
    bool IsQada) : DomainEvent;
