using PrayerCycle.Domain.FamilyMembers;

namespace PrayerCycle.Application.FamilyMembers.Dtos;

public sealed record FamilyMemberDto(
    Guid Id,
    Guid FamilyId,
    Guid? UserId,
    string Name,
    MemberRole Role,
    string AvatarColor,
    string? AvatarInitial,
    DateTime? BirthDate,
    bool IsActive,
    DateTime CreatedAt);

public static class FamilyMemberDtoMapper
{
    public static FamilyMemberDto ToDto(this FamilyMember member) =>
        new(
            member.Id.Value,
            member.FamilyId.Value,
            member.UserId?.Value,
            member.Name.Value,
            member.Role,
            member.AvatarColor.Value,
            member.AvatarInitial?.Value,
            member.BirthDate,
            member.IsActive,
            member.CreatedAt);
}
