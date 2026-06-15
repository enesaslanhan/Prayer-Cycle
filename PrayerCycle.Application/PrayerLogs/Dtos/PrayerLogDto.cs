using PrayerCycle.Domain.PrayerLogs;

namespace PrayerCycle.Application.PrayerLogs.Dtos;

public sealed record PrayerLogDto(
    Guid Id,
    Guid FamilyMemberId,
    DateOnly Date,
    PrayerTime PrayerTime,
    PrayerStatus Status,
    PrayerType? Type,
    bool IsQada,
    string? Notes,
    DateTime LoggedAt);

public static class PrayerLogDtoMapper
{
    public static PrayerLogDto ToDto(this PrayerLog prayerLog) =>
        new(
            prayerLog.Id.Value,
            prayerLog.FamilyMemberId.Value,
            prayerLog.Date,
            prayerLog.PrayerTime,
            prayerLog.Status,
            prayerLog.Type,
            prayerLog.IsQada,
            prayerLog.Notes?.Value,
            prayerLog.LoggedAt);
}
