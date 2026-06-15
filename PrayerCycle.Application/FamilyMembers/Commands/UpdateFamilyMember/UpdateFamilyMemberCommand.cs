using MediatR;

namespace PrayerCycle.Application.FamilyMembers.Commands.UpdateFamilyMember;

public sealed record UpdateFamilyMemberCommand(
    Guid Id,
    string Name,
    string AvatarColor,
    string? AvatarInitial,
    DateTime? BirthDate,
    bool IsActive) : IRequest;
