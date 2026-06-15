using MediatR;

namespace PrayerCycle.Application.PrayerLogs.Commands.DeletePrayerLog;

public sealed record DeletePrayerLogCommand(Guid Id) : IRequest;
