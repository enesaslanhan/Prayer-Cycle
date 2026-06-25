using MediatR;
using PrayerCycle.Application.Common.Abstractions;
using PrayerCycle.Application.Common.Exceptions;
using PrayerCycle.Application.Users.Dtos;
using PrayerCycle.Domain.Users;

namespace PrayerCycle.Application.Users.Commands.LoginUser;

public sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, UserDto>
{
    private readonly IUserReadRepository _readRepository;
    private readonly IUserWriteRepository _writeRepository;
    private readonly IPasswordHasher _passwordHasher;

    public LoginUserCommandHandler(
        IUserReadRepository readRepository,
        IUserWriteRepository writeRepository,
        IPasswordHasher passwordHasher)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);
        var user = await _readRepository.GetByEmailAsync(email, cancellationToken);

        if (user is null
            || user.PasswordHash is null
            || !_passwordHasher.Verify(request.Password, user.PasswordHash.Value))
        {
            throw new InvalidCredentialsException();
        }

        user.RecordLogin();
        await _writeRepository.UpdateAsync(user, cancellationToken);

        return user.ToDto();
    }
}
