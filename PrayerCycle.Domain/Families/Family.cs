using PrayerCycle.Domain.Common;
using PrayerCycle.Domain.Users;

namespace PrayerCycle.Domain.Families;

/// <summary>
/// Aile grubunu ve üyelik kurallarını yöneten aggregate root.
/// </summary>
public sealed class Family : AggregateRoot<FamilyId>
{
    /// <summary>
    /// Ailenin adı.
    /// </summary>
    public FamilyName Name { get; private set; } = null!;

    /// <summary>
    /// Ailenin sahibi olan kullanıcının kimliği.
    /// </summary>
    public UserId OwnerUserId { get; private set; }

    /// <summary>
    /// Ailenin oluşturulma zamanı (UTC).
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Aileye katılım için kullanılan davet kodu.
    /// </summary>
    public InviteCode? InviteCode { get; private set; }

    /// <summary>
    /// Aileye eklenebilecek maksimum üye sayısı.
    /// </summary>
    public MaxMembers MaxMembers { get; private set; } = null!;

    /// <summary>
    /// ORM ve serileştirme için gerekli olan parametresiz kurucu.
    /// </summary>
    private Family()
    {
    }

    /// <summary>
    /// Belirtilen sahip kullanıcı ile yeni bir aile oluşturur.
    /// </summary>
    /// <param name="ownerUserId">Aile sahibinin kullanıcı kimliği.</param>
    /// <param name="name">Aile adı.</param>
    /// <param name="maxMembers">Maksimum üye limiti. Belirtilmezse ücretsiz plan varsayılanı kullanılır.</param>
    /// <returns>Oluşturulan aile aggregate'i.</returns>
    public static Family Create(UserId ownerUserId, FamilyName name, MaxMembers? maxMembers = null)
    {
        var family = new Family
        {
            Id = FamilyId.New(),
            Name = name,
            OwnerUserId = ownerUserId,
            CreatedAt = DateTime.UtcNow,
            InviteCode = null,
            MaxMembers = maxMembers ?? MaxMembers.FreeTierDefault()
        };

        family.RaiseDomainEvent(new FamilyCreated(family.Id, ownerUserId, name));

        return family;
    }

    /// <summary>
    /// Ailenin adını günceller.
    /// </summary>
    /// <param name="name">Yeni aile adı.</param>
    public void ChangeName(FamilyName name)
    {
        Name = name;
    }

    /// <summary>
    /// Rastgele yeni bir davet kodu üretir ve aileye atar.
    /// </summary>
    public void GenerateInviteCode()
    {
        var inviteCode = InviteCode.Generate();
        InviteCode = inviteCode;

        RaiseDomainEvent(new FamilyInviteCodeSet(Id, inviteCode));
    }

    /// <summary>
    /// Belirtilen davet kodunu aileye atar.
    /// </summary>
    /// <param name="inviteCode">Atanacak davet kodu.</param>
    public void SetInviteCode(InviteCode inviteCode)
    {
        InviteCode = inviteCode;

        RaiseDomainEvent(new FamilyInviteCodeSet(Id, inviteCode));
    }

    /// <summary>
    /// Ailenin davet kodunu kaldırır.
    /// </summary>
    public void ClearInviteCode()
    {
        InviteCode = null;
    }

    /// <summary>
    /// Ailenin maksimum üye limitini günceller.
    /// </summary>
    /// <param name="maxMembers">Yeni üye limiti.</param>
    public void UpdateMaxMembers(MaxMembers maxMembers)
    {
        MaxMembers = maxMembers;
    }
}
