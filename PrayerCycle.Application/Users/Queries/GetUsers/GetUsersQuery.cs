using MediatR;
using PrayerCycle.Application.Users.Dtos;

namespace PrayerCycle.Application.Users.Queries.GetUsers;

public sealed record GetUsersQuery : IRequest<IReadOnlyList<UserDto>>;
