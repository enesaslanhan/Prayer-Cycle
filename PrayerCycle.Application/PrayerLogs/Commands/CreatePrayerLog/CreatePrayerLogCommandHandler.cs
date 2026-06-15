using MediatR;
using PrayerCycle.Application.FamilyMembers;
using PrayerCycle.Application.PrayerLogs.Dtos;
using PrayerCycle.Domain.Common;
using PrayerCycle.Domain.FamilyMembers;
using PrayerCycle.Domain.PrayerLogs;

namespace PrayerCycle.Application.PrayerLogs.Commands.CreatePrayerLog;

public sealed class CreatePrayerLogCommandHandler : IRequestHandler<CreatePrayerLogCommand, PrayerLogDto>
{
    private readonly IFamilyMemberReadRepository _familyMemberReadRepository;
    private readonly IPrayerLogWriteRepository _writeRepository;

    public CreatePrayerLogCommandHandler(
        IFamilyMemberReadRepository familyMemberReadRepository,
        IPrayerLogWriteRepository writeRepository)
    {
        _familyMemberReadRepository = familyMemberReadRepository;
        _writeRepository = writeRepository;
    }

    public async Task<PrayerLogDto> Handle(CreatePrayerLogCommand request, CancellationToken cancellationToken)
    {
        var familyMemberId = FamilyMemberId.From(request.FamilyMemberId);
        var familyMember = await _familyMemberReadRepository.GetByIdAsync(familyMemberId, cancellationToken);

        if (familyMember is null)
        {
            throw new DomainException($"Family member '{request.FamilyMemberId}' was not found.");
        }

        var notes = request.Notes is not null ? PrayerNotes.Create(request.Notes) : null;

        var prayerLog = PrayerLog.Create(
            familyMemberId,
            request.Date,
            request.PrayerTime,
            request.Status,
            request.Type,
            request.IsQada,
            notes);

        await _writeRepository.AddAsync(prayerLog, cancellationToken);

        return prayerLog.ToDto();
    }
}
