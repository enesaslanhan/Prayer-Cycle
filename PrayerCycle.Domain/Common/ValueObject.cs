namespace PrayerCycle.Domain.Common;

/// <summary>
/// Değer eşitliğine göre karşılaştırılan value object'ler için temel sınıf.
/// </summary>
/// <typeparam name="T">Value object'in sarmaladığı değer tipi.</typeparam>
public abstract class ValueObject<T> : IEquatable<ValueObject<T>>
    where T : notnull
{
    /// <summary>
    /// Value object'in temel değeri.
    /// </summary>
    public abstract T Value { get; }

    /// <summary>
    /// Belirtilen nesne ile bu value object'in eşit olup olmadığını kontrol eder.
    /// </summary>
    /// <param name="obj">Karşılaştırılacak nesne.</param>
    /// <returns>Eşitse <c>true</c>, değilse <c>false</c>.</returns>
    public override bool Equals(object? obj) =>
        obj is ValueObject<T> other && Equals(other);

    /// <summary>
    /// Belirtilen value object ile bu value object'in eşit olup olmadığını kontrol eder.
    /// </summary>
    /// <param name="other">Karşılaştırılacak value object.</param>
    /// <returns>Eşitse <c>true</c>, değilse <c>false</c>.</returns>
    public bool Equals(ValueObject<T>? other) =>
        other is not null && EqualityComparer<T>.Default.Equals(Value, other.Value);

    /// <summary>
    /// Value object için hash kodu üretir.
    /// </summary>
    /// <returns>Hash kodu.</returns>
    public override int GetHashCode() =>
        EqualityComparer<T>.Default.GetHashCode(Value);

    /// <summary>
    /// İki value object'in eşit olup olmadığını kontrol eder.
    /// </summary>
    /// <param name="left">Sol operand.</param>
    /// <param name="right">Sağ operand.</param>
    /// <returns>Eşitse <c>true</c>, değilse <c>false</c>.</returns>
    public static bool operator ==(ValueObject<T>? left, ValueObject<T>? right) =>
        Equals(left, right);

    /// <summary>
    /// İki value object'in eşit olmadığını kontrol eder.
    /// </summary>
    /// <param name="left">Sol operand.</param>
    /// <param name="right">Sağ operand.</param>
    /// <returns>Eşit değilse <c>true</c>, eşitse <c>false</c>.</returns>
    public static bool operator !=(ValueObject<T>? left, ValueObject<T>? right) =>
        !Equals(left, right);
}
