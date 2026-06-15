using MediatR;
using PrayerCycle.Domain.PrayerLogs;

namespace PrayerCycle.Application.PrayerLogs.Commands.UpdatePrayerLog;

public sealed record UpdatePrayerLogCommand(
    Guid Id,
    PrayerStatus Status,
    PrayerType? Type,
    bool IsQada,
    string? Notes) : IRequest;
