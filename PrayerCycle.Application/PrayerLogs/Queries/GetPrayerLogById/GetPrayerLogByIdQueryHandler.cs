using MediatR;
using PrayerCycle.Application.Common.Exceptions;
using PrayerCycle.Application.PrayerLogs.Dtos;
using PrayerCycle.Domain.PrayerLogs;

namespace PrayerCycle.Application.PrayerLogs.Queries.GetPrayerLogById;

public sealed class GetPrayerLogByIdQueryHandler : IRequestHandler<GetPrayerLogByIdQuery, PrayerLogDto>
{
    private readonly IPrayerLogReadRepository _readRepository;

    public GetPrayerLogByIdQueryHandler(IPrayerLogReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<PrayerLogDto> Handle(GetPrayerLogByIdQuery request, CancellationToken cancellationToken)
    {
        var prayerLog = await _readRepository.GetByIdAsync(PrayerLogId.From(request.Id), cancellationToken);

        if (prayerLog is null)
        {
            throw new NotFoundException(nameof(PrayerLog), request.Id);
        }

        return prayerLog.ToDto();
    }
}
