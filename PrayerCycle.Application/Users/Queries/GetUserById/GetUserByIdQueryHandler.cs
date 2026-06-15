using MediatR;
using PrayerCycle.Application.Common.Exceptions;
using PrayerCycle.Application.Users.Dtos;
using PrayerCycle.Domain.Users;

namespace PrayerCycle.Application.Users.Queries.GetUserById;

public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUserReadRepository _readRepository;

    public GetUserByIdQueryHandler(IUserReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _readRepository.GetByIdAsync(UserId.From(request.Id), cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        return user.ToDto();
    }
}
