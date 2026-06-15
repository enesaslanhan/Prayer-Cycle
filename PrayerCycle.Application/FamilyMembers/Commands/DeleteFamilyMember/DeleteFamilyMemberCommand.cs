using MediatR;

namespace PrayerCycle.Application.FamilyMembers.Commands.DeleteFamilyMember;

public sealed record DeleteFamilyMemberCommand(Guid Id) : IRequest;
