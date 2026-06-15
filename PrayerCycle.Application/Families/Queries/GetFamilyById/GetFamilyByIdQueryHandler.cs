using MediatR;
using PrayerCycle.Application.Common.Exceptions;
using PrayerCycle.Application.Families.Dtos;
using PrayerCycle.Domain.Families;

namespace PrayerCycle.Application.Families.Queries.GetFamilyById;

public sealed class GetFamilyByIdQueryHandler : IRequestHandler<GetFamilyByIdQuery, FamilyDto>
{
    private readonly IFamilyReadRepository _readRepository;

    public GetFamilyByIdQueryHandler(IFamilyReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<FamilyDto> Handle(GetFamilyByIdQuery request, CancellationToken cancellationToken)
    {
        var family = await _readRepository.GetByIdAsync(FamilyId.From(request.Id), cancellationToken);

        if (family is null)
        {
            throw new NotFoundException(nameof(Family), request.Id);
        }

        return family.ToDto();
    }
}
