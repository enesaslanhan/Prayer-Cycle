using MediatR;
using PrayerCycle.Application.Users.Dtos;

namespace PrayerCycle.Application.Users.Commands.LoginUser;

public sealed record LoginUserCommand(string Email, string Password) : IRequest<UserDto>;
