namespace PrayerCycle.Domain.Common;

/// <summary>
/// Domain içinde gerçekleşen anlamlı olayları temsil eden temel kayıt.
/// </summary>
public abstract record DomainEvent
{
    /// <summary>
    /// Event'in benzersiz kimliği.
    /// </summary>
    public Guid EventId { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Event'in UTC zaman damgası.
    /// </summary>
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
}
