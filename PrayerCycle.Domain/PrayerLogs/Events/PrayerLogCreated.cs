using PrayerCycle.Domain.Common;
using PrayerCycle.Domain.FamilyMembers;

namespace PrayerCycle.Domain.PrayerLogs;

/// <summary>
/// Yeni bir namaz kaydının oluşturulduğunu bildiren domain event.
/// </summary>
/// <param name="PrayerLogId">Oluşturulan namaz kaydının kimliği.</param>
/// <param name="FamilyMemberId">Kaydı oluşturan aile üyesinin kimliği.</param>
/// <param name="Date">Namazın kaydedildiği gün.</param>
/// <param name="PrayerTime">Namaz vakti.</param>
/// <param name="Status">Namaz durumu.</param>
public sealed record PrayerLogCreated(
    PrayerLogId PrayerLogId,
    FamilyMemberId FamilyMemberId,
    DateOnly Date,
    PrayerTime PrayerTime,
    PrayerStatus Status) : DomainEvent;
