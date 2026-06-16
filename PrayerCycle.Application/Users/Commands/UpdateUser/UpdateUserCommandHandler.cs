using MediatR;
using PrayerCycle.Application.Common.Abstractions;
using PrayerCycle.Application.Common.Exceptions;
using PrayerCycle.Domain.Users;

namespace PrayerCycle.Application.Users.Commands.UpdateUser;

public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IUserReadRepository _readRepository;
    private readonly IUserWriteRepository _writeRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UpdateUserCommandHandler(
        IUserReadRepository readRepository,
        IUserWriteRepository writeRepository,
        IPasswordHasher passwordHasher)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _passwordHasher = passwordHasher;
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

            if (request.Password is not null)
            {
                user.SetPasswordHash(HashedPassword.Create(_passwordHasher.Hash(request.Password)));
            }
        }

        await _writeRepository.UpdateAsync(user, cancellationToken);
    }
}
