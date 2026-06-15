using MediatR;
using PrayerCycle.Application.Common.Exceptions;
using PrayerCycle.Domain.FamilyMembers;

namespace PrayerCycle.Application.FamilyMembers.Commands.UpdateFamilyMember;

public sealed class UpdateFamilyMemberCommandHandler : IRequestHandler<UpdateFamilyMemberCommand>
{
    private readonly IFamilyMemberReadRepository _readRepository;
    private readonly IFamilyMemberWriteRepository _writeRepository;

    public UpdateFamilyMemberCommandHandler(
        IFamilyMemberReadRepository readRepository,
        IFamilyMemberWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task Handle(UpdateFamilyMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await _readRepository.GetByIdAsync(FamilyMemberId.From(request.Id), cancellationToken);

        if (member is null)
        {
            throw new NotFoundException(nameof(FamilyMember), request.Id);
        }

        if (request.IsActive && !member.IsActive)
        {
            member.Reactivate();
        }
        else if (!request.IsActive && member.IsActive)
        {
            member.Deactivate();
        }

        if (member.IsActive)
        {
            member.ChangeName(MemberName.Create(request.Name));
            member.UpdateAvatar(
                AvatarColor.Create(request.AvatarColor),
                request.AvatarInitial is not null ? AvatarInitial.Create(request.AvatarInitial) : null);
            member.SetBirthDate(request.BirthDate);
        }

        await _writeRepository.UpdateAsync(member, cancellationToken);
    }
}
