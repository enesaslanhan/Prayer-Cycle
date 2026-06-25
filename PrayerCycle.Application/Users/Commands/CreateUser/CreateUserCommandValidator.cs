using FluentValidation;
using PrayerCycle.Domain.Users;

namespace PrayerCycle.Application.Users.Commands.CreateUser;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(command => command.DisplayName)
            .NotEmpty()
            .MinimumLength(DisplayName.MinLength)
            .MaximumLength(DisplayName.MaxLength);

        RuleFor(command => command.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(128);
    }
}
