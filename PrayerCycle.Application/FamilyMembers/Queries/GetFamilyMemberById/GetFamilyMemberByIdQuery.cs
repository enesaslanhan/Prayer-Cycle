using MediatR;
using PrayerCycle.Application.FamilyMembers.Dtos;

namespace PrayerCycle.Application.FamilyMembers.Queries.GetFamilyMemberById;

public sealed record GetFamilyMemberByIdQuery(Guid Id) : IRequest<FamilyMemberDto>;
