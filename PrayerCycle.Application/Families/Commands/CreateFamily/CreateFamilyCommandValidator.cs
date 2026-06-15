using FluentValidation;
using PrayerCycle.Domain.Families;

namespace PrayerCycle.Application.Families.Commands.CreateFamily;

public sealed class CreateFamilyCommandValidator : AbstractValidator<CreateFamilyCommand>
{
    public CreateFamilyCommandValidator()
    {
        RuleFor(command => command.OwnerUserId).NotEmpty();

        RuleFor(command => command.Name)
            .NotEmpty()
            .MinimumLength(FamilyName.MinLength)
            .MaximumLength(FamilyName.MaxLength);

        RuleFor(command => command.MaxMembers)
            .GreaterThanOrEqualTo(1)
            .When(command => command.MaxMembers.HasValue);
    }
}
