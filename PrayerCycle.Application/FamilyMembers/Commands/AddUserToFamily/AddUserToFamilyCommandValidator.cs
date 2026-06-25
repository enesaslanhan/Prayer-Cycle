using FluentValidation;
using PrayerCycle.Domain.FamilyMembers;
using PrayerCycle.Domain.Users;

namespace PrayerCycle.Application.FamilyMembers.Commands.AddUserToFamily;

public sealed class AddUserToFamilyCommandValidator : AbstractValidator<AddUserToFamilyCommand>
{
    public AddUserToFamilyCommandValidator()
    {
        RuleFor(command => command.FamilyId).NotEmpty();

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
