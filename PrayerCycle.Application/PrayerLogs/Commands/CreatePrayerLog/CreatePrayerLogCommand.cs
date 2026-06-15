using MediatR;
using PrayerCycle.Application.PrayerLogs.Dtos;
using PrayerCycle.Domain.PrayerLogs;

namespace PrayerCycle.Application.PrayerLogs.Commands.CreatePrayerLog;

public sealed record CreatePrayerLogCommand(
    Guid FamilyMemberId,
    DateOnly Date,
    PrayerTime PrayerTime,
    PrayerStatus Status,
    PrayerType? Type,
    bool IsQada,
    string? Notes) : IRequest<PrayerLogDto>;
