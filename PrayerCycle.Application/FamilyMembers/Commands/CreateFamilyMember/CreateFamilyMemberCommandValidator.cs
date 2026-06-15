using FluentValidation;
using PrayerCycle.Domain.FamilyMembers;

namespace PrayerCycle.Application.FamilyMembers.Commands.CreateFamilyMember;

public sealed class CreateFamilyMemberCommandValidator : AbstractValidator<CreateFamilyMemberCommand>
{
    public CreateFamilyMemberCommandValidator()
    {
        RuleFor(command => command.FamilyId).NotEmpty();

        RuleFor(command => command.UserId)
            .NotEmpty()
            .When(command => command.Role == MemberRole.Parent);

        RuleFor(command => command.Name)
            .NotEmpty()
            .MinimumLength(MemberName.MinLength)
            .MaximumLength(MemberName.MaxLength);

        RuleFor(command => command.Role).IsInEnum();

        RuleFor(command => command.AvatarColor).NotEmpty();

        RuleFor(command => command.AvatarInitial)
            .Length(1)
            .When(command => command.AvatarInitial is not null);

        RuleFor(command => command.BirthDate)
            .LessThanOrEqualTo(_ => DateTime.UtcNow.Date)
            .When(command => command.BirthDate.HasValue);
    }
}
