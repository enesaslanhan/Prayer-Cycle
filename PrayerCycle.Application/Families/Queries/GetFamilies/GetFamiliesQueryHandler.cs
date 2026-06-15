using MediatR;
using PrayerCycle.Application.Families.Dtos;
using PrayerCycle.Domain.Users;

namespace PrayerCycle.Application.Families.Queries.GetFamilies;

public sealed class GetFamiliesQueryHandler : IRequestHandler<GetFamiliesQuery, IReadOnlyList<FamilyDto>>
{
    private readonly IFamilyReadRepository _readRepository;

    public GetFamiliesQueryHandler(IFamilyReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<IReadOnlyList<FamilyDto>> Handle(GetFamiliesQuery request, CancellationToken cancellationToken)
    {
        var families = request.OwnerUserId.HasValue
            ? await _readRepository.GetByOwnerUserIdAsync(UserId.From(request.OwnerUserId.Value), cancellationToken)
            : await _readRepository.GetAllAsync(cancellationToken);

        return families.Select(family => family.ToDto()).ToList();
    }
}
