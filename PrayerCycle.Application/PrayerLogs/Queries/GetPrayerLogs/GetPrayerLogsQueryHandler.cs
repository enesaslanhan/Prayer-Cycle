using MediatR;
using PrayerCycle.Application.PrayerLogs.Dtos;
using PrayerCycle.Domain.FamilyMembers;

namespace PrayerCycle.Application.PrayerLogs.Queries.GetPrayerLogs;

public sealed class GetPrayerLogsQueryHandler : IRequestHandler<GetPrayerLogsQuery, IReadOnlyList<PrayerLogDto>>
{
    private readonly IPrayerLogReadRepository _readRepository;

    public GetPrayerLogsQueryHandler(IPrayerLogReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<IReadOnlyList<PrayerLogDto>> Handle(GetPrayerLogsQuery request, CancellationToken cancellationToken)
    {
        var familyMemberId = FamilyMemberId.From(request.FamilyMemberId);

        var prayerLogs = request.Date.HasValue
            ? await _readRepository.GetByFamilyMemberIdAndDateAsync(familyMemberId, request.Date.Value, cancellationToken)
            : await _readRepository.GetByFamilyMemberIdAsync(familyMemberId, cancellationToken);

        return prayerLogs.Select(prayerLog => prayerLog.ToDto()).ToList();
    }
}
