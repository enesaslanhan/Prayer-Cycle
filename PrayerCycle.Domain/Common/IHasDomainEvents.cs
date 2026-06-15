namespace PrayerCycle.Domain.Common;

/// <summary>
/// Domain event toplayabilen tipler için sözleşme.
/// </summary>
public interface IHasDomainEvents
{
    /// <summary>
    /// Henüz işlenmemiş domain event'lerin koleksiyonu.
    /// </summary>
    IReadOnlyCollection<DomainEvent> DomainEvents { get; }

    /// <summary>
    /// Toplanan tüm domain event'leri temizler.
    /// </summary>
    void ClearDomainEvents();
}
