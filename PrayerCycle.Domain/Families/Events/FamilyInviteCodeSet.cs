using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.Families;

/// <summary>
/// Bir aileye davet kodu atandığını veya güncellendiğini bildiren domain event.
/// </summary>
/// <param name="FamilyId">Davet kodu atanan ailenin kimliği.</param>
/// <param name="InviteCode">Atanan davet kodu.</param>
public sealed record FamilyInviteCodeSet(FamilyId FamilyId, InviteCode InviteCode) : DomainEvent;
