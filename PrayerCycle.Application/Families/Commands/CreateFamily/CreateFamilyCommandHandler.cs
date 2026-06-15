using MediatR;
using PrayerCycle.Application.Families.Dtos;
using PrayerCycle.Application.Users;
using PrayerCycle.Domain.Common;
using PrayerCycle.Domain.Families;
using PrayerCycle.Domain.Users;

namespace PrayerCycle.Application.Families.Commands.CreateFamily;

public sealed class CreateFamilyCommandHandler : IRequestHandler<CreateFamilyCommand, FamilyDto>
{
    private readonly IFamilyWriteRepository _writeRepository;
    private readonly IUserReadRepository _userReadRepository;

    public CreateFamilyCommandHandler(
        IFamilyWriteRepository writeRepository,
        IUserReadRepository userReadRepository)
    {
        _writeRepository = writeRepository;
        _userReadRepository = userReadRepository;
    }

    public async Task<FamilyDto> Handle(CreateFamilyCommand request, CancellationToken cancellationToken)
    {
        var ownerUserId = UserId.From(request.OwnerUserId);
        var owner = await _userReadRepository.GetByIdAsync(ownerUserId, cancellationToken);

        if (owner is null)
        {
            throw new DomainException($"Owner user '{request.OwnerUserId}' was not found.");
        }

        var maxMembers = request.MaxMembers.HasValue
            ? MaxMembers.Create(request.MaxMembers.Value)
            : null;

        var family = Family.Create(ownerUserId, FamilyName.Create(request.Name), maxMembers);

        await _writeRepository.AddAsync(family, cancellationToken);

        return family.ToDto();
    }
}
