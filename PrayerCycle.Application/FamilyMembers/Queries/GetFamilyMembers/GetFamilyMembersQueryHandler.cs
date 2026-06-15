using MediatR;
using PrayerCycle.Application.FamilyMembers.Dtos;
using PrayerCycle.Domain.Families;

namespace PrayerCycle.Application.FamilyMembers.Queries.GetFamilyMembers;

public sealed class GetFamilyMembersQueryHandler : IRequestHandler<GetFamilyMembersQuery, IReadOnlyList<FamilyMemberDto>>
{
    private readonly IFamilyMemberReadRepository _readRepository;

    public GetFamilyMembersQueryHandler(IFamilyMemberReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<IReadOnlyList<FamilyMemberDto>> Handle(GetFamilyMembersQuery request, CancellationToken cancellationToken)
    {
        var members = await _readRepository.GetByFamilyIdAsync(
            FamilyId.From(request.FamilyId),
            cancellationToken);

        return members.Select(member => member.ToDto()).ToList();
    }
}
