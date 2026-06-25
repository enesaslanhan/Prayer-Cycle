using PrayerCycle.Application.Users.Dtos;

namespace PrayerCycle.Application.FamilyMembers.Dtos;

public sealed record AddUserToFamilyResultDto(
    UserDto User,
    FamilyMemberDto Member);
