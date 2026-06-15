using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.FamilyMembers;

/// <summary>
/// Aile üyesi avatar baş harfini temsil eden value object.
/// </summary>
public sealed class AvatarInitial : ValueObject<string>
{
    /// <summary>
    /// Avatar baş harfi değeri.
    /// </summary>
    public override string Value { get; }

    /// <summary>
    /// Doğrulanmış baş harf değeriyle yeni bir <see cref="AvatarInitial"/> örneği oluşturur.
    /// </summary>
    /// <param name="value">Tek karakterlik baş harf.</param>
    private AvatarInitial(string value) => Value = value;

    /// <summary>
    /// Ham metinden geçerli bir <see cref="AvatarInitial"/> value object oluşturur.
    /// </summary>
    /// <param name="rawInitial">Kullanıcıdan alınan baş harf metni.</param>
    /// <returns>Oluşturulan <see cref="AvatarInitial"/> değeri.</returns>
    /// <exception cref="InvalidAvatarInitialException">Baş harf geçersizse fırlatılır.</exception>
    public static AvatarInitial Create(string rawInitial)
    {
        if (string.IsNullOrWhiteSpace(rawInitial))
        {
            throw new InvalidAvatarInitialException("Avatar initial cannot be empty.");
        }

        var trimmed = rawInitial.Trim();

        if (trimmed.Length != 1)
        {
            throw new InvalidAvatarInitialException("Avatar initial must be exactly one character.");
        }

        return new AvatarInitial(trimmed.ToUpperInvariant());
    }
}
