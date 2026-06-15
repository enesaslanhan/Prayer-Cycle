namespace PrayerCycle.Domain.PrayerLogs;

/// <summary>
/// Namaz kaydı aggregate'ine ait strongly-typed kimlik değeri.
/// </summary>
/// <param name="Value">Namaz kaydının Guid kimliği.</param>
public readonly record struct PrayerLogId(Guid Value)
{
    /// <summary>
    /// Yeni bir benzersiz namaz kaydı kimliği oluşturur.
    /// </summary>
    /// <returns>Yeni <see cref="PrayerLogId"/> değeri.</returns>
    public static PrayerLogId New() => new(Guid.NewGuid());

    /// <summary>
    /// Mevcut bir Guid değerinden namaz kaydı kimliği oluşturur.
    /// </summary>
    /// <param name="value">Guid kimlik değeri.</param>
    /// <returns>Oluşturulan <see cref="PrayerLogId"/> değeri.</returns>
    /// <exception cref="ArgumentException"><paramref name="value"/> boş Guid ise fırlatılır.</exception>
    public static PrayerLogId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Prayer log id cannot be empty.", nameof(value));
        }

        return new PrayerLogId(value);
    }

    /// <summary>
    /// Kimlik değerinin string temsilini döndürür.
    /// </summary>
    /// <returns>Guid string değeri.</returns>
    public override string ToString() => Value.ToString();
}
