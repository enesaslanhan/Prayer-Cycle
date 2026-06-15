using System.Text.RegularExpressions;
using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.FamilyMembers;

/// <summary>
/// Aile üyesi avatar rengini temsil eden value object.
/// </summary>
public sealed partial class AvatarColor : ValueObject<string>
{
    /// <summary>
    /// Normalize edilmiş hex renk kodu değeri.
    /// </summary>
    public override string Value { get; }

    /// <summary>
    /// Doğrulanmış renk kodu değeriyle yeni bir <see cref="AvatarColor"/> örneği oluşturur.
    /// </summary>
    /// <param name="value">Büyük harfe normalize edilmiş hex renk kodu.</param>
    private AvatarColor(string value) => Value = value;

    /// <summary>
    /// Ham metinden geçerli bir <see cref="AvatarColor"/> value object oluşturur.
    /// </summary>
    /// <param name="rawColor">Kullanıcıdan alınan renk kodu. Örnek: "#22C55E".</param>
    /// <returns>Oluşturulan <see cref="AvatarColor"/> değeri.</returns>
    /// <exception cref="InvalidAvatarColorException">Renk kodu geçersizse fırlatılır.</exception>
    public static AvatarColor Create(string rawColor)
    {
        if (string.IsNullOrWhiteSpace(rawColor))
        {
            throw new InvalidAvatarColorException("Avatar color cannot be empty.");
        }

        var normalized = rawColor.Trim().ToUpperInvariant();

        if (!AvatarColorFormatRegex().IsMatch(normalized))
        {
            throw new InvalidAvatarColorException("Avatar color must be a valid hex color code (e.g. #22C55E).");
        }

        return new AvatarColor(normalized);
    }

    /// <summary>
    /// Avatar rengi formatını doğrulamak için kullanılan regex desenini döndürür.
    /// </summary>
    /// <returns>Hex renk format regex'i.</returns>
    [GeneratedRegex(@"^#([0-9A-F]{6}|[0-9A-F]{3})$", RegexOptions.CultureInvariant)]
    private static partial Regex AvatarColorFormatRegex();
}
