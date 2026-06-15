using MediatR;

namespace PrayerCycle.Application.Users.Commands.DeleteUser;

public sealed record DeleteUserCommand(Guid Id) : IRequest;
