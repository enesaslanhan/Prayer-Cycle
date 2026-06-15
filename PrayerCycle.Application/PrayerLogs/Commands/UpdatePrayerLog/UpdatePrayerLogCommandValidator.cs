using FluentValidation;
using PrayerCycle.Domain.PrayerLogs;

namespace PrayerCycle.Application.PrayerLogs.Commands.UpdatePrayerLog;

public sealed class UpdatePrayerLogCommandValidator : AbstractValidator<UpdatePrayerLogCommand>
{
    public UpdatePrayerLogCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty();

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
