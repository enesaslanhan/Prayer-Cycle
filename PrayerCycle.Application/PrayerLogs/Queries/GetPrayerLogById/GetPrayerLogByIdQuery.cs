using MediatR;
using PrayerCycle.Application.PrayerLogs.Dtos;

namespace PrayerCycle.Application.PrayerLogs.Queries.GetPrayerLogById;

public sealed record GetPrayerLogByIdQuery(Guid Id) : IRequest<PrayerLogDto>;
