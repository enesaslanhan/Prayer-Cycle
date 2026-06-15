using PrayerCycle.Domain.Common;
using PrayerCycle.Domain.Families;

namespace PrayerCycle.Domain.FamilyMembers;

/// <summary>
/// Yeni bir aile üyesinin başarıyla oluşturulduğunu bildiren domain event.
/// </summary>
/// <param name="FamilyMemberId">Oluşturulan aile üyesinin kimliği.</param>
/// <param name="FamilyId">Üyenin bağlı olduğu ailenin kimliği.</param>
/// <param name="Role">Üyenin aile içindeki rolü.</param>
/// <param name="Name">Üyenin adı.</param>
public sealed record FamilyMemberCreated(
    FamilyMemberId FamilyMemberId,
    FamilyId FamilyId,
    MemberRole Role,
    MemberName Name) : DomainEvent;
