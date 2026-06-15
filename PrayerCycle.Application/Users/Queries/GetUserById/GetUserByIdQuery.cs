using MediatR;
using PrayerCycle.Application.Users.Dtos;

namespace PrayerCycle.Application.Users.Queries.GetUserById;

public sealed record GetUserByIdQuery(Guid Id) : IRequest<UserDto>;
