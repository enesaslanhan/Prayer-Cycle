using MediatR;
using PrayerCycle.Application.Users.Dtos;

namespace PrayerCycle.Application.Users.Commands.CreateUser;

public sealed record CreateUserCommand(
    string Email,
    string DisplayName,
    string? Password) : IRequest<UserDto>;
