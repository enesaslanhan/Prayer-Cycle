using MediatR;
using PrayerCycle.Application.PrayerLogs.Dtos;

namespace PrayerCycle.Application.PrayerLogs.Queries.GetPrayerLogs;

public sealed record GetPrayerLogsQuery(Guid FamilyMemberId, DateOnly? Date = null) : IRequest<IReadOnlyList<PrayerLogDto>>;
