using FluentValidation;

namespace PrayerCycle.Application.PrayerLogs.Queries.GetPrayerLogs;

public sealed class GetPrayerLogsQueryValidator : AbstractValidator<GetPrayerLogsQuery>
{
    public GetPrayerLogsQueryValidator()
    {
        RuleFor(query => query.FamilyMemberId).NotEmpty();
    }
}
