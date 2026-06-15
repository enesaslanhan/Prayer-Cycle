using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.Families;

/// <summary>
/// Aileye eklenebilecek maksimum üye sayısını temsil eden value object.
/// </summary>
public sealed class MaxMembers : ValueObject<int>
{
    /// <summary>
    /// Ücretsiz plan için varsayılan üye limiti.
    /// </summary>
    public const int FreeTierLimit = 3;

    private const int UnlimitedValue = int.MaxValue;

    /// <summary>
    /// Maksimum üye sayısı değeri.
    /// </summary>
    public override int Value { get; }

    /// <summary>
    /// Üye limitinin sınırsız olup olmadığını belirtir.
    /// </summary>
    public bool IsUnlimited => Value == UnlimitedValue;

    /// <summary>
    /// Belirtilen üye limiti değeriyle yeni bir <see cref="MaxMembers"/> örneği oluşturur.
    /// </summary>
    /// <param name="value">Maksimum üye sayısı.</param>
    private MaxMembers(int value) => Value = value;

    /// <summary>
    /// Ücretsiz plan için varsayılan üye limitini döndürür.
    /// </summary>
    /// <returns>Ücretsiz plan <see cref="MaxMembers"/> değeri.</returns>
    public static MaxMembers FreeTierDefault() => Create(FreeTierLimit);

    /// <summary>
    /// Sınırsız üye limitini temsil eden değeri döndürür.
    /// </summary>
    /// <returns>Sınırsız <see cref="MaxMembers"/> değeri.</returns>
    public static MaxMembers Unlimited() => new(UnlimitedValue);

    /// <summary>
    /// Belirtilen sayıdan geçerli bir <see cref="MaxMembers"/> value object oluşturur.
    /// </summary>
    /// <param name="value">Maksimum üye sayısı.</param>
    /// <returns>Oluşturulan <see cref="MaxMembers"/> değeri.</returns>
    /// <exception cref="InvalidMaxMembersException">Değer 1'den küçükse fırlatılır.</exception>
    public static MaxMembers Create(int value)
    {
        if (value < 1)
        {
            throw new InvalidMaxMembersException("Max members must be at least 1.");
        }

        return new MaxMembers(value);
    }
}
