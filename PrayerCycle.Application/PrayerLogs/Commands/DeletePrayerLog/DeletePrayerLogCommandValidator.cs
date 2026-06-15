using FluentValidation;

namespace PrayerCycle.Application.PrayerLogs.Commands.DeletePrayerLog;

public sealed class DeletePrayerLogCommandValidator : AbstractValidator<DeletePrayerLogCommand>
{
    public DeletePrayerLogCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty();
    }
}
