using PrayerCycle.Domain.Common;
using PrayerCycle.Domain.Families;
using PrayerCycle.Domain.Users;

namespace PrayerCycle.Domain.FamilyMembers;

/// <summary>
/// Aile içindeki bireysel üye profilini yöneten aggregate root.
/// </summary>
public sealed class FamilyMember : AggregateRoot<FamilyMemberId>
{
    /// <summary>
    /// Üyenin bağlı olduğu ailenin kimliği.
    /// </summary>
    public FamilyId FamilyId { get; private set; }

    /// <summary>
    /// Yetişkin hesaba bağlı kullanıcı kimliği. Çocuk profillerinde null olabilir.
    /// </summary>
    public UserId? UserId { get; private set; }

    /// <summary>
    /// Üyenin adı.
    /// </summary>
    public MemberName Name { get; private set; } = null!;

    /// <summary>
    /// Üyenin aile içindeki rolü.
    /// </summary>
    public MemberRole Role { get; private set; }

    /// <summary>
    /// Üye avatar rengi.
    /// </summary>
    public AvatarColor AvatarColor { get; private set; } = null!;

    /// <summary>
    /// Üye avatar baş harfi.
    /// </summary>
    public AvatarInitial? AvatarInitial { get; private set; }

    /// <summary>
    /// Üyenin doğum tarihi.
    /// </summary>
    public DateTime? BirthDate { get; private set; }

    /// <summary>
    /// Üyenin aktif olup olmadığını belirtir.
    /// </summary>
    public bool IsActive { get; private set; }

    /// <summary>
    /// Üyenin oluşturulma zamanı (UTC).
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// ORM ve serileştirme için gerekli olan parametresiz kurucu.
    /// </summary>
    private FamilyMember()
    {
    }

    /// <summary>
    /// Kullanıcı hesabına bağlı bir ebeveyn profili oluşturur.
    /// </summary>
    /// <param name="familyId">Ailenin kimliği.</param>
    /// <param name="userId">Ebeveynin kullanıcı kimliği.</param>
    /// <param name="name">Üye adı.</param>
    /// <param name="avatarColor">Avatar rengi.</param>
    /// <param name="avatarInitial">Opsiyonel avatar baş harfi.</param>
    /// <param name="birthDate">Opsiyonel doğum tarihi.</param>
    /// <returns>Oluşturulan aile üyesi aggregate'i.</returns>
    public static FamilyMember CreateParent(
        FamilyId familyId,
        UserId userId,
        MemberName name,
        AvatarColor avatarColor,
        AvatarInitial? avatarInitial = null,
        DateTime? birthDate = null)
    {
        ValidateBirthDate(birthDate);

        var member = new FamilyMember
        {
            Id = FamilyMemberId.New(),
            FamilyId = familyId,
            UserId = userId,
            Name = name,
            Role = MemberRole.Parent,
            AvatarColor = avatarColor,
            AvatarInitial = avatarInitial,
            BirthDate = birthDate,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        member.RaiseDomainEvent(new FamilyMemberCreated(member.Id, familyId, MemberRole.Parent, name));

        return member;
    }

    /// <summary>
    /// Kullanıcı hesabı olmayan bir çocuk profili oluşturur.
    /// </summary>
    /// <param name="familyId">Ailenin kimliği.</param>
    /// <param name="name">Üye adı.</param>
    /// <param name="avatarColor">Avatar rengi.</param>
    /// <param name="avatarInitial">Opsiyonel avatar baş harfi.</param>
    /// <param name="birthDate">Opsiyonel doğum tarihi.</param>
    /// <returns>Oluşturulan aile üyesi aggregate'i.</returns>
    public static FamilyMember CreateChild(
        FamilyId familyId,
        MemberName name,
        AvatarColor avatarColor,
        AvatarInitial? avatarInitial = null,
        DateTime? birthDate = null)
    {
        ValidateBirthDate(birthDate);

        var member = new FamilyMember
        {
            Id = FamilyMemberId.New(),
            FamilyId = familyId,
            UserId = null,
            Name = name,
            Role = MemberRole.Child,
            AvatarColor = avatarColor,
            AvatarInitial = avatarInitial,
            BirthDate = birthDate,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        member.RaiseDomainEvent(new FamilyMemberCreated(member.Id, familyId, MemberRole.Child, name));

        return member;
    }

    /// <summary>
    /// Çocuk profilini bir kullanıcı hesabına bağlar.
    /// </summary>
    /// <param name="userId">Bağlanacak kullanıcı kimliği.</param>
    /// <exception cref="FamilyMemberInactiveException">Üye aktif değilse fırlatılır.</exception>
    /// <exception cref="InvalidMemberRoleException">Üye zaten bir hesaba bağlıysa fırlatılır.</exception>
    public void LinkToUser(UserId userId)
    {
        EnsureActive();

        if (UserId is not null)
        {
            throw new InvalidMemberRoleException("Family member is already linked to a user account.");
        }

        UserId = userId;

        RaiseDomainEvent(new FamilyMemberLinkedToUser(Id, userId));
    }

    /// <summary>
    /// Aktif üyenin adını günceller.
    /// </summary>
    /// <param name="name">Yeni üye adı.</param>
    /// <exception cref="FamilyMemberInactiveException">Üye aktif değilse fırlatılır.</exception>
    public void ChangeName(MemberName name)
    {
        EnsureActive();
        Name = name;
    }

    /// <summary>
    /// Aktif üyenin avatar bilgilerini günceller.
    /// </summary>
    /// <param name="avatarColor">Yeni avatar rengi.</param>
    /// <param name="avatarInitial">Yeni avatar baş harfi.</param>
    /// <exception cref="FamilyMemberInactiveException">Üye aktif değilse fırlatılır.</exception>
    public void UpdateAvatar(AvatarColor avatarColor, AvatarInitial? avatarInitial = null)
    {
        EnsureActive();
        AvatarColor = avatarColor;
        AvatarInitial = avatarInitial;
    }

    /// <summary>
    /// Aktif üyenin doğum tarihini günceller.
    /// </summary>
    /// <param name="birthDate">Yeni doğum tarihi.</param>
    /// <exception cref="FamilyMemberInactiveException">Üye aktif değilse fırlatılır.</exception>
    /// <exception cref="InvalidBirthDateException">Doğum tarihi gelecekteyse fırlatılır.</exception>
    public void SetBirthDate(DateTime? birthDate)
    {
        EnsureActive();
        ValidateBirthDate(birthDate);
        BirthDate = birthDate;
    }

    /// <summary>
    /// Üyeyi pasif duruma alır.
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
    }

    /// <summary>
    /// Pasif üyeyi tekrar aktif duruma alır.
    /// </summary>
    public void Reactivate()
    {
        IsActive = true;
    }

    /// <summary>
    /// Üyenin aktif olduğunu doğrular; değilse exception fırlatır.
    /// </summary>
    /// <exception cref="FamilyMemberInactiveException">Üye aktif değilse fırlatılır.</exception>
    private void EnsureActive()
    {
        if (!IsActive)
        {
            throw new FamilyMemberInactiveException();
        }
    }

    /// <summary>
    /// Doğum tarihinin gelecekte olmadığını doğrular.
    /// </summary>
    /// <param name="birthDate">Doğrulanacak doğum tarihi.</param>
    /// <exception cref="InvalidBirthDateException">Doğum tarihi gelecekteyse fırlatılır.</exception>
    private static void ValidateBirthDate(DateTime? birthDate)
    {
        if (birthDate is null)
        {
            return;
        }

        if (birthDate.Value.Date > DateTime.UtcNow.Date)
        {
            throw new InvalidBirthDateException("Birth date cannot be in the future.");
        }
    }
}
