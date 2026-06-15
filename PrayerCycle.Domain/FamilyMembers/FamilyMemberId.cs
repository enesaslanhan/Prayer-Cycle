namespace PrayerCycle.Domain.FamilyMembers;

/// <summary>
/// Aile üyesi aggregate'ine ait strongly-typed kimlik değeri.
/// </summary>
/// <param name="Value">Aile üyesinin Guid kimliği.</param>
public readonly record struct FamilyMemberId(Guid Value)
{
    /// <summary>
    /// Yeni bir benzersiz aile üyesi kimliği oluşturur.
    /// </summary>
    /// <returns>Yeni <see cref="FamilyMemberId"/> değeri.</returns>
    public static FamilyMemberId New() => new(Guid.NewGuid());

    /// <summary>
    /// Mevcut bir Guid değerinden aile üyesi kimliği oluşturur.
    /// </summary>
    /// <param name="value">Guid kimlik değeri.</param>
    /// <returns>Oluşturulan <see cref="FamilyMemberId"/> değeri.</returns>
    /// <exception cref="ArgumentException"><paramref name="value"/> boş Guid ise fırlatılır.</exception>
    public static FamilyMemberId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Family member id cannot be empty.", nameof(value));
        }

        return new FamilyMemberId(value);
    }

    /// <summary>
    /// Kimlik değerinin string temsilini döndürür.
    /// </summary>
    /// <returns>Guid string değeri.</returns>
    public override string ToString() => Value.ToString();
}
