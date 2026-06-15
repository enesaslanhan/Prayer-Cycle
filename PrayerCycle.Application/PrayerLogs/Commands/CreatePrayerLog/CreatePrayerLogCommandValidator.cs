using FluentValidation;
using PrayerCycle.Domain.PrayerLogs;

namespace PrayerCycle.Application.PrayerLogs.Commands.CreatePrayerLog;

public sealed class CreatePrayerLogCommandValidator : AbstractValidator<CreatePrayerLogCommand>
{
    public CreatePrayerLogCommandValidator()
    {
        RuleFor(command => command.FamilyMemberId).NotEmpty();

        RuleFor(command => command.Date)
            .LessThanOrEqualTo(_ => DateOnly.FromDateTime(DateTime.UtcNow));

        RuleFor(command => command.PrayerTime).IsInEnum();
        RuleFor(command => command.Status).IsInEnum();

        RuleFor(command => command.Type)
            .IsInEnum()
            .When(command => command.Type.HasValue);

        RuleFor(command => command.Type)
            .Null()
            .When(command => command.Status == PrayerStatus.Missed);

        RuleFor(command => command.Notes)
            .MaximumLength(PrayerNotes.MaxLength)
            .When(command => command.Notes is not null);
    }
}
