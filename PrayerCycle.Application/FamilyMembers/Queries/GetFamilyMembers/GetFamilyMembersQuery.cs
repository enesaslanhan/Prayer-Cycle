using MediatR;
using PrayerCycle.Application.FamilyMembers.Dtos;

namespace PrayerCycle.Application.FamilyMembers.Queries.GetFamilyMembers;

public sealed record GetFamilyMembersQuery(Guid FamilyId) : IRequest<IReadOnlyList<FamilyMemberDto>>;
