using FluentValidation;

namespace PrayerCycle.Application.PrayerLogs.Queries.GetPrayerLogById;

public sealed class GetPrayerLogByIdQueryValidator : AbstractValidator<GetPrayerLogByIdQuery>
{
    public GetPrayerLogByIdQueryValidator()
    {
        RuleFor(query => query.Id).NotEmpty();
    }
}
