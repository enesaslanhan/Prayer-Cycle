using MediatR;
using PrayerCycle.Application.Families.Dtos;

namespace PrayerCycle.Application.Families.Queries.GetFamilyById;

public sealed record GetFamilyByIdQuery(Guid Id) : IRequest<FamilyDto>;
