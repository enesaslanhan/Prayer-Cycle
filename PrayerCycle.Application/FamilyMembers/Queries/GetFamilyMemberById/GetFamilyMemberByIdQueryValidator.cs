using FluentValidation;

namespace PrayerCycle.Application.FamilyMembers.Queries.GetFamilyMemberById;

public sealed class GetFamilyMemberByIdQueryValidator : AbstractValidator<GetFamilyMemberByIdQuery>
{
    public GetFamilyMemberByIdQueryValidator()
    {
        RuleFor(query => query.Id).NotEmpty();
    }
}
