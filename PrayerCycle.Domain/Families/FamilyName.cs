using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.Families;

/// <summary>
/// Aile adını temsil eden value object.
/// </summary>
public sealed class FamilyName : ValueObject<string>
{
    /// <summary>
    /// Aile adı için minimum karakter uzunluğu.
    /// </summary>
    public const int MinLength = 2;

    /// <summary>
    /// Aile adı için maksimum karakter uzunluğu.
    /// </summary>
    public const int MaxLength = 100;

    /// <summary>
    /// Doğrulanmış aile adı değeri.
    /// </summary>
    public override string Value { get; }

    /// <summary>
    /// Doğrulanmış aile adı değeriyle yeni bir <see cref="FamilyName"/> örneği oluşturur.
    /// </summary>
    /// <param name="value">Trimlenmiş aile adı.</param>
    private FamilyName(string value) => Value = value;

    /// <summary>
    /// Ham metinden geçerli bir <see cref="FamilyName"/> value object oluşturur.
    /// </summary>
    /// <param name="rawName">Kullanıcıdan alınan aile adı metni.</param>
    /// <returns>Oluşturulan <see cref="FamilyName"/> değeri.</returns>
    /// <exception cref="InvalidFamilyNameException">Aile adı geçersizse fırlatılır.</exception>
    public static FamilyName Create(string rawName)
    {
        if (string.IsNullOrWhiteSpace(rawName))
        {
            throw new InvalidFamilyNameException("Family name cannot be empty.");
        }

        var trimmed = rawName.Trim();

        if (trimmed.Length < MinLength)
        {
            throw new InvalidFamilyNameException($"Family name must be at least {MinLength} characters.");
        }

        if (trimmed.Length > MaxLength)
        {
            throw new InvalidFamilyNameException($"Family name cannot exceed {MaxLength} characters.");
        }

        return new FamilyName(trimmed);
    }
}
