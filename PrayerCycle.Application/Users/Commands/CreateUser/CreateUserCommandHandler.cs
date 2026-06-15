using MediatR;
using PrayerCycle.Application.Users.Dtos;
using PrayerCycle.Domain.Users;

namespace PrayerCycle.Application.Users.Commands.CreateUser;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserReadRepository _readRepository;
    private readonly IUserWriteRepository _writeRepository;

    public CreateUserCommandHandler(
        IUserReadRepository readRepository,
        IUserWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);
        var displayName = DisplayName.Create(request.DisplayName);

        var existingUser = await _readRepository.GetByEmailAsync(email, cancellationToken);
        if (existingUser is not null)
        {
            throw new InvalidOperationException("A user with this email already exists.");
        }

        var user = request.PasswordHash is not null
            ? User.RegisterWithPassword(email, HashedPassword.Create(request.PasswordHash), displayName)
            : User.RegisterWithGoogle(email, displayName);

        await _writeRepository.AddAsync(user, cancellationToken);

        return user.ToDto();
    }
}
