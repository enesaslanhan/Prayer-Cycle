using FluentValidation;

namespace PrayerCycle.Application.Families.Commands.DeleteFamily;

public sealed class DeleteFamilyCommandValidator : AbstractValidator<DeleteFamilyCommand>
{
    public DeleteFamilyCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty();
    }
}
