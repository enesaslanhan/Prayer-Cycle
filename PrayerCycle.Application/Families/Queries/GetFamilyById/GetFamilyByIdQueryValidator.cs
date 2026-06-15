using FluentValidation;

namespace PrayerCycle.Application.Families.Queries.GetFamilyById;

public sealed class GetFamilyByIdQueryValidator : AbstractValidator<GetFamilyByIdQuery>
{
    public GetFamilyByIdQueryValidator()
    {
        RuleFor(query => query.Id).NotEmpty();
    }
}
