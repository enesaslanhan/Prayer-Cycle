using FluentValidation;
using PrayerCycle.Domain.FamilyMembers;

namespace PrayerCycle.Application.FamilyMembers.Commands.UpdateFamilyMember;

public sealed class UpdateFamilyMemberCommandValidator : AbstractValidator<UpdateFamilyMemberCommand>
{
    public UpdateFamilyMemberCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty();

        RuleFor(command => command.Name)
            .NotEmpty()
            .MinimumLength(MemberName.MinLength)
            .MaximumLength(MemberName.MaxLength);

        RuleFor(command => command.AvatarColor).NotEmpty();

        RuleFor(command => command.AvatarInitial)
            .Length(1)
            .When(command => command.AvatarInitial is not null);

        RuleFor(command => command.BirthDate)
            .LessThanOrEqualTo(_ => DateTime.UtcNow.Date)
            .When(command => command.BirthDate.HasValue);
    }
}
