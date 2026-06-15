using MediatR;
using PrayerCycle.Application.Common.Exceptions;
using PrayerCycle.Application.FamilyMembers.Dtos;
using PrayerCycle.Domain.FamilyMembers;

namespace PrayerCycle.Application.FamilyMembers.Queries.GetFamilyMemberById;

public sealed class GetFamilyMemberByIdQueryHandler : IRequestHandler<GetFamilyMemberByIdQuery, FamilyMemberDto>
{
    private readonly IFamilyMemberReadRepository _readRepository;

    public GetFamilyMemberByIdQueryHandler(IFamilyMemberReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<FamilyMemberDto> Handle(GetFamilyMemberByIdQuery request, CancellationToken cancellationToken)
    {
        var member = await _readRepository.GetByIdAsync(FamilyMemberId.From(request.Id), cancellationToken);

        if (member is null)
        {
            throw new NotFoundException(nameof(FamilyMember), request.Id);
        }

        return member.ToDto();
    }
}
