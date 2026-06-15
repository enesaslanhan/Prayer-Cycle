using MediatR;
using PrayerCycle.Application.Common.Exceptions;
using PrayerCycle.Domain.Users;

namespace PrayerCycle.Application.Users.Commands.DeleteUser;

public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserReadRepository _readRepository;
    private readonly IUserWriteRepository _writeRepository;

    public DeleteUserCommandHandler(
        IUserReadRepository readRepository,
        IUserWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _readRepository.GetByIdAsync(UserId.From(request.Id), cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        await _writeRepository.RemoveAsync(user, cancellationToken);
    }
}
