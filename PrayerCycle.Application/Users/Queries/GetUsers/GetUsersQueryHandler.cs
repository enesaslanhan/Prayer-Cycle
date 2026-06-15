using MediatR;
using PrayerCycle.Application.Users.Dtos;

namespace PrayerCycle.Application.Users.Queries.GetUsers;

public sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IReadOnlyList<UserDto>>
{
    private readonly IUserReadRepository _readRepository;

    public GetUsersQueryHandler(IUserReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<IReadOnlyList<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _readRepository.GetAllAsync(cancellationToken);
        return users.Select(user => user.ToDto()).ToList();
    }
}
