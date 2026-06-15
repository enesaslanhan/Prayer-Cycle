using PrayerCycle.Domain.Common;
using PrayerCycle.Domain.Users;

namespace PrayerCycle.Domain.FamilyMembers;

/// <summary>
/// Bir aile üyesinin kullanıcı hesabına bağlandığını bildiren domain event.
/// </summary>
/// <param name="FamilyMemberId">Bağlanan aile üyesinin kimliği.</param>
/// <param name="UserId">Bağlanan kullanıcı kimliği.</param>
public sealed record FamilyMemberLinkedToUser(FamilyMemberId FamilyMemberId, UserId UserId) : DomainEvent;
