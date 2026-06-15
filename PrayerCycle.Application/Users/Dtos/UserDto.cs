using PrayerCycle.Domain.Users;

namespace PrayerCycle.Application.Users.Dtos;

public sealed record UserDto(
    Guid Id,
    string Email,
    string DisplayName,
    DateTime CreatedAt,
    DateTime? LastLoginAt,
    bool IsActive,
    bool HasPassword);

public static class UserDtoMapper
{
    public static UserDto ToDto(this User user) =>
        new(
            user.Id.Value,
            user.Email.Value,
            user.DisplayName.Value,
            user.CreatedAt,
            user.LastLoginAt,
            user.IsActive,
            user.PasswordHash is not null);
}
