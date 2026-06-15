using FluentValidation;

namespace PrayerCycle.Application.Families.Queries.GetFamilies;

public sealed class GetFamiliesQueryValidator : AbstractValidator<GetFamiliesQuery>
{
    public GetFamiliesQueryValidator()
    {
        RuleFor(query => query.OwnerUserId)
            .NotEmpty()
            .When(query => query.OwnerUserId.HasValue);
    }
}
