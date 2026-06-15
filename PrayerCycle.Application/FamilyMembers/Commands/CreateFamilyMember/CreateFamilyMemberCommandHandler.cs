using MediatR;
using PrayerCycle.Application.Families;
using PrayerCycle.Application.FamilyMembers.Dtos;
using PrayerCycle.Application.Users;
using PrayerCycle.Domain.Common;
using PrayerCycle.Domain.Families;
using PrayerCycle.Domain.FamilyMembers;
using PrayerCycle.Domain.Users;

namespace PrayerCycle.Application.FamilyMembers.Commands.CreateFamilyMember;

public sealed class CreateFamilyMemberCommandHandler : IRequestHandler<CreateFamilyMemberCommand, FamilyMemberDto>
{
    private readonly IFamilyReadRepository _familyReadRepository;
    private readonly IUserReadRepository _userReadRepository;
    private readonly IFamilyMemberWriteRepository _writeRepository;

    public CreateFamilyMemberCommandHandler(
        IFamilyReadRepository familyReadRepository,
        IUserReadRepository userReadRepository,
        IFamilyMemberWriteRepository writeRepository)
    {
        _familyReadRepository = familyReadRepository;
        _userReadRepository = userReadRepository;
        _writeRepository = writeRepository;
    }

    public async Task<FamilyMemberDto> Handle(CreateFamilyMemberCommand request, CancellationToken cancellationToken)
    {
        var familyId = FamilyId.From(request.FamilyId);
        var family = await _familyReadRepository.GetByIdAsync(familyId, cancellationToken);

        if (family is null)
        {
            throw new DomainException($"Family '{request.FamilyId}' was not found.");
        }

        var name = MemberName.Create(request.Name);
        var avatarColor = AvatarColor.Create(request.AvatarColor);
        var avatarInitial = request.AvatarInitial is not null
            ? AvatarInitial.Create(request.AvatarInitial)
            : null;

        FamilyMember member = request.Role switch
        {
            MemberRole.Parent when request.UserId.HasValue =>
                await CreateParentAsync(request, familyId, name, avatarColor, avatarInitial, cancellationToken),
            MemberRole.Child =>
                FamilyMember.CreateChild(familyId, name, avatarColor, avatarInitial, request.BirthDate),
            _ => throw new DomainException("Parent members must have a linked user id.")
        };

        await _writeRepository.AddAsync(member, cancellationToken);

        return member.ToDto();
    }

    private async Task<FamilyMember> CreateParentAsync(
        CreateFamilyMemberCommand request,
        FamilyId familyId,
        MemberName name,
        AvatarColor avatarColor,
        AvatarInitial? avatarInitial,
        CancellationToken cancellationToken)
    {
        var userId = UserId.From(request.UserId!.Value);
        var user = await _userReadRepository.GetByIdAsync(userId, cancellationToken);

        if (user is null)
        {
            throw new DomainException($"User '{request.UserId}' was not found.");
        }

        return FamilyMember.CreateParent(
            familyId,
            userId,
            name,
            avatarColor,
            avatarInitial,
            request.BirthDate);
    }
}
