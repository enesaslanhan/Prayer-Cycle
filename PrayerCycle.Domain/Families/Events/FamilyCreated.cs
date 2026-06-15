using PrayerCycle.Domain.Common;
using PrayerCycle.Domain.Users;

namespace PrayerCycle.Domain.Families;

/// <summary>
/// Yeni bir ailenin başarıyla oluşturulduğunu bildiren domain event.
/// </summary>
/// <param name="FamilyId">Oluşturulan ailenin kimliği.</param>
/// <param name="OwnerUserId">Aile sahibinin kullanıcı kimliği.</param>
/// <param name="Name">Ailenin adı.</param>
public sealed record FamilyCreated(FamilyId FamilyId, UserId OwnerUserId, FamilyName Name) : DomainEvent;
