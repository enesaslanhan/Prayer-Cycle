using MediatR;
using PrayerCycle.Application.Common.Exceptions;
using PrayerCycle.Domain.FamilyMembers;

namespace PrayerCycle.Application.FamilyMembers.Commands.DeleteFamilyMember;

public sealed class DeleteFamilyMemberCommandHandler : IRequestHandler<DeleteFamilyMemberCommand>
{
    private readonly IFamilyMemberReadRepository _readRepository;
    private readonly IFamilyMemberWriteRepository _writeRepository;

    public DeleteFamilyMemberCommandHandler(
        IFamilyMemberReadRepository readRepository,
        IFamilyMemberWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task Handle(DeleteFamilyMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await _readRepository.GetByIdAsync(FamilyMemberId.From(request.Id), cancellationToken);

        if (member is null)
        {
            throw new NotFoundException(nameof(FamilyMember), request.Id);
        }

        await _writeRepository.RemoveAsync(member, cancellationToken);
    }
}
