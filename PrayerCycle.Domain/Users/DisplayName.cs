using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.Users;

/// <summary>
/// Kullanıcının görünen adını temsil eden value object.
/// </summary>
public sealed class DisplayName : ValueObject<string>
{
    /// <summary>
    /// Görünen ad için minimum karakter uzunluğu.
    /// </summary>
    public const int MinLength = 2;

    /// <summary>
    /// Görünen ad için maksimum karakter uzunluğu.
    /// </summary>
    public const int MaxLength = 50;

    /// <summary>
    /// Doğrulanmış görünen ad değeri.
    /// </summary>
    public override string Value { get; }

    /// <summary>
    /// Doğrulanmış görünen ad değeriyle yeni bir <see cref="DisplayName"/> örneği oluşturur.
    /// </summary>
    /// <param name="value">Trimlenmiş görünen ad.</param>
    private DisplayName(string value) => Value = value;

    /// <summary>
    /// Ham metinden geçerli bir <see cref="DisplayName"/> value object oluşturur.
    /// </summary>
    /// <param name="rawName">Kullanıcıdan alınan görünen ad metni.</param>
    /// <returns>Oluşturulan <see cref="DisplayName"/> değeri.</returns>
    /// <exception cref="InvalidDisplayNameException">Görünen ad geçersizse fırlatılır.</exception>
    public static DisplayName Create(string rawName)
    {
        if (string.IsNullOrWhiteSpace(rawName))
        {
            throw new InvalidDisplayNameException("Display name cannot be empty.");
        }

        var trimmed = rawName.Trim();

        if (trimmed.Length < MinLength)
        {
            throw new InvalidDisplayNameException($"Display name must be at least {MinLength} characters.");
        }

        if (trimmed.Length > MaxLength)
        {
            throw new InvalidDisplayNameException($"Display name cannot exceed {MaxLength} characters.");
        }

        return new DisplayName(trimmed);
    }
}
