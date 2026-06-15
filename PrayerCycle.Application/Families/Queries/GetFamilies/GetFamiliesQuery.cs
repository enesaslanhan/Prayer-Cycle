using MediatR;
using PrayerCycle.Application.Families.Dtos;

namespace PrayerCycle.Application.Families.Queries.GetFamilies;

public sealed record GetFamiliesQuery(Guid? OwnerUserId = null) : IRequest<IReadOnlyList<FamilyDto>>;
