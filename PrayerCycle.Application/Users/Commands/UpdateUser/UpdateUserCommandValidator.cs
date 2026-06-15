using FluentValidation;
using PrayerCycle.Domain.Users;

namespace PrayerCycle.Application.Users.Commands.UpdateUser;

public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();

        RuleFor(command => command.DisplayName)
            .NotEmpty()
            .MinimumLength(DisplayName.MinLength)
            .MaximumLength(DisplayName.MaxLength);

        RuleFor(command => command.PasswordHash)
            .NotEmpty()
            .When(command => command.PasswordHash is not null);
    }
}
