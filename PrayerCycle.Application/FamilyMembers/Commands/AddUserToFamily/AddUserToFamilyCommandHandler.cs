using MediatR;
using PrayerCycle.Application.Common.Abstractions;
using PrayerCycle.Application.Families;
using PrayerCycle.Application.FamilyMembers.Dtos;
using PrayerCycle.Application.Users;
using PrayerCycle.Application.Users.Dtos;
using PrayerCycle.Domain.Common;
using PrayerCycle.Domain.Families;
using PrayerCycle.Domain.FamilyMembers;
using PrayerCycle.Domain.Users;

namespace PrayerCycle.Application.FamilyMembers.Commands.AddUserToFamily;

public sealed class AddUserToFamilyCommandHandler : IRequestHandler<AddUserToFamilyCommand, AddUserToFamilyResultDto>
{
    private readonly IFamilyReadRepository _familyReadRepository;
    private readonly IFamilyMemberReadRepository _familyMemberReadRepository;
    private readonly IUserReadRepository _userReadRepository;
    private readonly IUserWriteRepository _userWriteRepository;
    private readonly IFamilyMemberWriteRepository _familyMemberWriteRepository;
    private readonly IPasswordHasher _passwordHasher;

    public AddUserToFamilyCommandHandler(
        IFamilyReadRepository familyReadRepository,
        IFamilyMemberReadRepository familyMemberReadRepository,
        IUserReadRepository userReadRepository,
        IUserWriteRepository userWriteRepository,
        IFamilyMemberWriteRepository familyMemberWriteRepository,
        IPasswordHasher passwordHasher)
    {
        _familyReadRepository = familyReadRepository;
        _familyMemberReadRepository = familyMemberReadRepository;
        _userReadRepository = userReadRepository;
        _userWriteRepository = userWriteRepository;
        _familyMemberWriteRepository = familyMemberWriteRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<AddUserToFamilyResultDto> Handle(
        AddUserToFamilyCommand request,
        CancellationToken cancellationToken)
    {
        var familyId = FamilyId.From(request.FamilyId);
        var family = await _familyReadRepository.GetByIdAsync(familyId, cancellationToken);

        if (family is null)
        {
            throw new DomainException($"Family '{request.FamilyId}' was not found.");
        }

        await EnsureMemberLimitNotReachedAsync(family, familyId, cancellationToken);

        var email = Email.Create(request.Email);
        var existingUser = await _userReadRepository.GetByEmailAsync(email, cancellationToken);

        if (existingUser is not null)
        {
            throw new DomainException("A user with this email already exists.");
        }

        var displayName = DisplayName.Create(request.DisplayName);
        var passwordHash = HashedPassword.Create(_passwordHasher.Hash(request.Password));
        var user = User.RegisterWithPassword(email, passwordHash, displayName);

        var name = MemberName.Create(request.Name);
        var avatarColor = AvatarColor.Create(request.AvatarColor);
        var avatarInitial = request.AvatarInitial is not null
            ? AvatarInitial.Create(request.AvatarInitial)
            : null;

        var member = FamilyMember.CreateParent(
            familyId,
            user.Id,
            name,
            avatarColor,
            avatarInitial,
            request.BirthDate);

        await _userWriteRepository.AddAsync(user, cancellationToken);
        await _familyMemberWriteRepository.AddAsync(member, cancellationToken);

        return new AddUserToFamilyResultDto(user.ToDto(), member.ToDto());
    }

    private async Task EnsureMemberLimitNotReachedAsync(
        Family family,
        FamilyId familyId,
        CancellationToken cancellationToken)
    {
        if (family.MaxMembers.IsUnlimited)
        {
            return;
        }

        var members = await _familyMemberReadRepository.GetByFamilyIdAsync(familyId, cancellationToken);
        var activeMemberCount = members.Count(member => member.IsActive);

        if (activeMemberCount >= family.MaxMembers.Value)
        {
            throw new DomainException(
                $"Family has reached the maximum member limit of {family.MaxMembers.Value}.");
        }
    }
}
