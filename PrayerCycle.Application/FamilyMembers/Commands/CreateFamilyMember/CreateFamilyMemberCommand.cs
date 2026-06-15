using MediatR;
using PrayerCycle.Application.FamilyMembers.Dtos;
using PrayerCycle.Domain.FamilyMembers;

namespace PrayerCycle.Application.FamilyMembers.Commands.CreateFamilyMember;

public sealed record CreateFamilyMemberCommand(
    Guid FamilyId,
    Guid? UserId,
    string Name,
    MemberRole Role,
    string AvatarColor,
    string? AvatarInitial,
    DateTime? BirthDate) : IRequest<FamilyMemberDto>;
