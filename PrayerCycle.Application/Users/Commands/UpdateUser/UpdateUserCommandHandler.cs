using MediatR;
using PrayerCycle.Application.Common.Exceptions;
using PrayerCycle.Domain.Users;

namespace PrayerCycle.Application.Users.Commands.UpdateUser;

public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IUserReadRepository _readRepository;
    private readonly IUserWriteRepository _writeRepository;

    public UpdateUserCommandHandler(
        IUserReadRepository readRepository,
        IUserWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _readRepository.GetByIdAsync(UserId.From(request.Id), cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        if (request.IsActive && !user.IsActive)
        {
            user.Reactivate();
        }
        else if (!request.IsActive && user.IsActive)
        {
            user.Deactivate();
        }

        if (user.IsActive)
        {
            user.ChangeDisplayName(DisplayName.Create(request.DisplayName));

            if (request.PasswordHash is not null)
            {
                user.SetPasswordHash(HashedPassword.Create(request.PasswordHash));
            }
        }

        await _writeRepository.UpdateAsync(user, cancellationToken);
    }
}
