namespace PrayerCycle.Domain.Common;

/// <summary>
/// Benzersiz kimliğe sahip domain entity'leri için temel sınıf.
/// </summary>
/// <typeparam name="TId">Entity kimlik tipi.</typeparam>
public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : notnull
{
    /// <summary>
    /// Entity'nin benzersiz kimliği.
    /// </summary>
    public TId Id { get; protected set; } = default!;

    /// <summary>
    /// Belirtilen nesne ile bu entity'nin eşit olup olmadığını kimlik üzerinden kontrol eder.
    /// </summary>
    /// <param name="obj">Karşılaştırılacak nesne.</param>
    /// <returns>Eşitse <c>true</c>, değilse <c>false</c>.</returns>
    public override bool Equals(object? obj) =>
        obj is Entity<TId> other && Equals(other);

    /// <summary>
    /// Belirtilen entity ile bu entity'nin eşit olup olmadığını kimlik üzerinden kontrol eder.
    /// </summary>
    /// <param name="other">Karşılaştırılacak entity.</param>
    /// <returns>Eşitse <c>true</c>, değilse <c>false</c>.</returns>
    public bool Equals(Entity<TId>? other) =>
        other is not null && EqualityComparer<TId>.Default.Equals(Id, other.Id);

    /// <summary>
    /// Entity kimliğine göre hash kodu üretir.
    /// </summary>
    /// <returns>Hash kodu.</returns>
    public override int GetHashCode() =>
        EqualityComparer<TId>.Default.GetHashCode(Id);

    /// <summary>
    /// İki entity'nin kimlik üzerinden eşit olup olmadığını kontrol eder.
    /// </summary>
    /// <param name="left">Sol operand.</param>
    /// <param name="right">Sağ operand.</param>
    /// <returns>Eşitse <c>true</c>, değilse <c>false</c>.</returns>
    public static bool operator ==(Entity<TId>? left, Entity<TId>? right) =>
        Equals(left, right);

    /// <summary>
    /// İki entity'nin kimlik üzerinden eşit olmadığını kontrol eder.
    /// </summary>
    /// <param name="left">Sol operand.</param>
    /// <param name="right">Sağ operand.</param>
    /// <returns>Eşit değilse <c>true</c>, eşitse <c>false</c>.</returns>
    public static bool operator !=(Entity<TId>? left, Entity<TId>? right) =>
        !Equals(left, right);
}
