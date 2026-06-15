using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.FamilyMembers;

/// <summary>
/// Aile üyesinin adını temsil eden value object.
/// </summary>
public sealed class MemberName : ValueObject<string>
{
    /// <summary>
    /// Üye adı için minimum karakter uzunluğu.
    /// </summary>
    public const int MinLength = 2;

    /// <summary>
    /// Üye adı için maksimum karakter uzunluğu.
    /// </summary>
    public const int MaxLength = 50;

    /// <summary>
    /// Doğrulanmış üye adı değeri.
    /// </summary>
    public override string Value { get; }

    /// <summary>
    /// Doğrulanmış üye adı değeriyle yeni bir <see cref="MemberName"/> örneği oluşturur.
    /// </summary>
    /// <param name="value">Trimlenmiş üye adı.</param>
    private MemberName(string value) => Value = value;

    /// <summary>
    /// Ham metinden geçerli bir <see cref="MemberName"/> value object oluşturur.
    /// </summary>
    /// <param name="rawName">Kullanıcıdan alınan üye adı metni.</param>
    /// <returns>Oluşturulan <see cref="MemberName"/> değeri.</returns>
    /// <exception cref="InvalidMemberNameException">Üye adı geçersizse fırlatılır.</exception>
    public static MemberName Create(string rawName)
    {
        if (string.IsNullOrWhiteSpace(rawName))
        {
            throw new InvalidMemberNameException("Member name cannot be empty.");
        }

        var trimmed = rawName.Trim();

        if (trimmed.Length < MinLength)
        {
            throw new InvalidMemberNameException($"Member name must be at least {MinLength} characters.");
        }

        if (trimmed.Length > MaxLength)
        {
            throw new InvalidMemberNameException($"Member name cannot exceed {MaxLength} characters.");
        }

        return new MemberName(trimmed);
    }
}
