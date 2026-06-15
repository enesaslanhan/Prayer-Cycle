using MediatR;
using PrayerCycle.Application.Common.Exceptions;
using PrayerCycle.Domain.Families;

namespace PrayerCycle.Application.Families.Commands.DeleteFamily;

public sealed class DeleteFamilyCommandHandler : IRequestHandler<DeleteFamilyCommand>
{
    private readonly IFamilyReadRepository _readRepository;
    private readonly IFamilyWriteRepository _writeRepository;

    public DeleteFamilyCommandHandler(
        IFamilyReadRepository readRepository,
        IFamilyWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task Handle(DeleteFamilyCommand request, CancellationToken cancellationToken)
    {
        var family = await _readRepository.GetByIdAsync(FamilyId.From(request.Id), cancellationToken);

        if (family is null)
        {
            throw new NotFoundException(nameof(Family), request.Id);
        }

        await _writeRepository.RemoveAsync(family, cancellationToken);
    }
}
