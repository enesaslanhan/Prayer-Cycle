using PrayerCycle.Domain.Families;

namespace PrayerCycle.Application.Families.Dtos;

public sealed record FamilyDto(
    Guid Id,
    string Name,
    Guid OwnerUserId,
    DateTime CreatedAt,
    string? InviteCode,
    int MaxMembers,
    bool IsUnlimited);

public static class FamilyDtoMapper
{
    public static FamilyDto ToDto(this Family family) =>
        new(
            family.Id.Value,
            family.Name.Value,
            family.OwnerUserId.Value,
            family.CreatedAt,
            family.InviteCode?.Value,
            family.MaxMembers.Value,
            family.MaxMembers.IsUnlimited);
}
