namespace PrayerCycle.Domain.Users;

/// <summary>
/// Kullanıcı aggregate'ine ait strongly-typed kimlik değeri.
/// </summary>
/// <param name="Value">Kullanıcının Guid kimliği.</param>
public readonly record struct UserId(Guid Value)
{
    /// <summary>
    /// Yeni bir benzersiz kullanıcı kimliği oluşturur.
    /// </summary>
    /// <returns>Yeni <see cref="UserId"/> değeri.</returns>
    public static UserId New() => new(Guid.NewGuid());

    /// <summary>
    /// Mevcut bir Guid değerinden kullanıcı kimliği oluşturur.
    /// </summary>
    /// <param name="value">Guid kimlik değeri.</param>
    /// <returns>Oluşturulan <see cref="UserId"/> değeri.</returns>
    /// <exception cref="ArgumentException"><paramref name="value"/> boş Guid ise fırlatılır.</exception>
    public static UserId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("User id cannot be empty.", nameof(value));
        }

        return new UserId(value);
    }

    /// <summary>
    /// Kimlik değerinin string temsilini döndürür.
    /// </summary>
    /// <returns>Guid string değeri.</returns>
    public override string ToString() => Value.ToString();
}
