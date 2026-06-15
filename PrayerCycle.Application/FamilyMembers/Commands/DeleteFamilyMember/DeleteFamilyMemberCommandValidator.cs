using FluentValidation;

namespace PrayerCycle.Application.FamilyMembers.Commands.DeleteFamilyMember;

public sealed class DeleteFamilyMemberCommandValidator : AbstractValidator<DeleteFamilyMemberCommand>
{
    public DeleteFamilyMemberCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty();
    }
}
