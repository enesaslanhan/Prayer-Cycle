using MediatR;

namespace PrayerCycle.Application.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand(
    Guid Id,
    string DisplayName,
    string? Password,
    bool IsActive) : IRequest;
