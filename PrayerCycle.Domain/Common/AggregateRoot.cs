namespace PrayerCycle.Domain.Common;

/// <summary>
/// Domain event yayınlayabilen aggregate root entity'leri için temel sınıf.
/// </summary>
/// <typeparam name="TId">Aggregate kimlik tipi.</typeparam>
public abstract class AggregateRoot<TId> : Entity<TId>, IHasDomainEvents
    where TId : notnull
{
    private readonly List<DomainEvent> _domainEvents = [];

    /// <summary>
    /// Aggregate tarafından yayınlanan ve henüz işlenmemiş domain event'ler.
    /// </summary>
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Yeni bir domain event'i aggregate'in event listesine ekler.
    /// </summary>
    /// <param name="domainEvent">Yayınlanacak domain event.</param>
    protected void RaiseDomainEvent(DomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);

    /// <summary>
    /// Aggregate üzerindeki tüm domain event'leri temizler.
    /// </summary>
    public void ClearDomainEvents() =>
        _domainEvents.Clear();
}
