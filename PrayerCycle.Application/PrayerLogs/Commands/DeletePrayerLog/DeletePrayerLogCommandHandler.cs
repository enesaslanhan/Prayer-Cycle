using MediatR;
using PrayerCycle.Application.Common.Exceptions;
using PrayerCycle.Domain.PrayerLogs;

namespace PrayerCycle.Application.PrayerLogs.Commands.DeletePrayerLog;

public sealed class DeletePrayerLogCommandHandler : IRequestHandler<DeletePrayerLogCommand>
{
    private readonly IPrayerLogReadRepository _readRepository;
    private readonly IPrayerLogWriteRepository _writeRepository;

    public DeletePrayerLogCommandHandler(
        IPrayerLogReadRepository readRepository,
        IPrayerLogWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task Handle(DeletePrayerLogCommand request, CancellationToken cancellationToken)
    {
        var prayerLog = await _readRepository.GetByIdAsync(PrayerLogId.From(request.Id), cancellationToken);

        if (prayerLog is null)
        {
            throw new NotFoundException(nameof(PrayerLog), request.Id);
        }

        await _writeRepository.RemoveAsync(prayerLog, cancellationToken);
    }
}
