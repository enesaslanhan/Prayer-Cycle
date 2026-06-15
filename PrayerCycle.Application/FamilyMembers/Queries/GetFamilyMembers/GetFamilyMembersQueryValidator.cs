using FluentValidation;

namespace PrayerCycle.Application.FamilyMembers.Queries.GetFamilyMembers;

public sealed class GetFamilyMembersQueryValidator : AbstractValidator<GetFamilyMembersQuery>
{
    public GetFamilyMembersQueryValidator()
    {
        RuleFor(query => query.FamilyId).NotEmpty();
    }
}
