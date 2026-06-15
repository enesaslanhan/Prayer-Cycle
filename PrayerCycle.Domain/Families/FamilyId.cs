namespace PrayerCycle.Domain.Families;

/// <summary>
/// Aile aggregate'ine ait strongly-typed kimlik değeri.
/// </summary>
/// <param name="Value">Ailenin Guid kimliği.</param>
public readonly record struct FamilyId(Guid Value)
{
    /// <summary>
    /// Yeni bir benzersiz aile kimliği oluşturur.
    /// </summary>
    /// <returns>Yeni <see cref="FamilyId"/> değeri.</returns>
    public static FamilyId New() => new(Guid.NewGuid());

    /// <summary>
    /// Mevcut bir Guid değerinden aile kimliği oluşturur.
    /// </summary>
    /// <param name="value">Guid kimlik değeri.</param>
    /// <returns>Oluşturulan <see cref="FamilyId"/> değeri.</returns>
    /// <exception cref="ArgumentException"><paramref name="value"/> boş Guid ise fırlatılır.</exception>
    public static FamilyId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Family id cannot be empty.", nameof(value));
        }

        return new FamilyId(value);
    }

    /// <summary>
    /// Kimlik değerinin string temsilini döndürür.
    /// </summary>
    /// <returns>Guid string değeri.</returns>
    public override string ToString() => Value.ToString();
}
