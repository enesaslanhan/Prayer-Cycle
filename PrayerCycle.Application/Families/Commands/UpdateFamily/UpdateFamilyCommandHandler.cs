using MediatR;
using PrayerCycle.Application.Common.Exceptions;
using PrayerCycle.Domain.Families;

namespace PrayerCycle.Application.Families.Commands.UpdateFamily;

public sealed class UpdateFamilyCommandHandler : IRequestHandler<UpdateFamilyCommand>
{
    private readonly IFamilyReadRepository _readRepository;
    private readonly IFamilyWriteRepository _writeRepository;

    public UpdateFamilyCommandHandler(
        IFamilyReadRepository readRepository,
        IFamilyWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task Handle(UpdateFamilyCommand request, CancellationToken cancellationToken)
    {
        var family = await _readRepository.GetByIdAsync(FamilyId.From(request.Id), cancellationToken);

        if (family is null)
        {
            throw new NotFoundException(nameof(Family), request.Id);
        }

        family.ChangeName(FamilyName.Create(request.Name));
        family.UpdateMaxMembers(MaxMembers.Create(request.MaxMembers));

        if (request.InviteCode is null)
        {
            family.ClearInviteCode();
        }
        else
        {
            family.SetInviteCode(InviteCode.Create(request.InviteCode));
        }

        await _writeRepository.UpdateAsync(family, cancellationToken);
    }
}
