using MediatR;
using PrayerCycle.Application.Common.Exceptions;
using PrayerCycle.Domain.PrayerLogs;

namespace PrayerCycle.Application.PrayerLogs.Commands.UpdatePrayerLog;

public sealed class UpdatePrayerLogCommandHandler : IRequestHandler<UpdatePrayerLogCommand>
{
    private readonly IPrayerLogReadRepository _readRepository;
    private readonly IPrayerLogWriteRepository _writeRepository;

    public UpdatePrayerLogCommandHandler(
        IPrayerLogReadRepository readRepository,
        IPrayerLogWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task Handle(UpdatePrayerLogCommand request, CancellationToken cancellationToken)
    {
        var prayerLog = await _readRepository.GetByIdAsync(PrayerLogId.From(request.Id), cancellationToken);

        if (prayerLog is null)
        {
            throw new NotFoundException(nameof(PrayerLog), request.Id);
        }

        prayerLog.UpdateStatus(request.Status, request.Type);
        prayerLog.SetQada(request.IsQada);

        var notes = request.Notes is not null ? PrayerNotes.Create(request.Notes) : null;
        prayerLog.UpdateNotes(notes);

        await _writeRepository.UpdateAsync(prayerLog, cancellationToken);
    }
}
