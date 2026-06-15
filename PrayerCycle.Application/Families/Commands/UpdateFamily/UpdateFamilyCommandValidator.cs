using FluentValidation;
using PrayerCycle.Domain.Families;

namespace PrayerCycle.Application.Families.Commands.UpdateFamily;

public sealed class UpdateFamilyCommandValidator : AbstractValidator<UpdateFamilyCommand>
{
    public UpdateFamilyCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty();

        RuleFor(command => command.Name)
            .NotEmpty()
            .MinimumLength(FamilyName.MinLength)
            .MaximumLength(FamilyName.MaxLength);

        RuleFor(command => command.MaxMembers).GreaterThanOrEqualTo(1);

        RuleFor(command => command.InviteCode)
            .NotEmpty()
            .When(command => command.InviteCode is not null);
    }
}
