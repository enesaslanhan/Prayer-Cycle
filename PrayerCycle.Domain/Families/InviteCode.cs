using System.Text.RegularExpressions;
using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.Families;

/// <summary>
/// Aileye katılım için kullanılan davet kodunu temsil eden value object.
/// </summary>
public sealed partial class InviteCode : ValueObject<string>
{
    /// <summary>
    /// Davet kodunun sabit uzunluğu.
    /// </summary>
    public const int Length = 8;

    private const string AllowedCharacters = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";

    /// <summary>
    /// Normalize edilmiş davet kodu değeri.
    /// </summary>
    public override string Value { get; }

    /// <summary>
    /// Doğrulanmış davet kodu değeriyle yeni bir <see cref="InviteCode"/> örneği oluşturur.
    /// </summary>
    /// <param name="value">Büyük harfe normalize edilmiş davet kodu.</param>
    private InviteCode(string value) => Value = value;

    /// <summary>
    /// Ham metinden geçerli bir <see cref="InviteCode"/> value object oluşturur.
    /// </summary>
    /// <param name="rawCode">Kullanıcıdan alınan davet kodu metni.</param>
    /// <returns>Oluşturulan <see cref="InviteCode"/> değeri.</returns>
    /// <exception cref="InvalidInviteCodeException">Davet kodu geçersizse fırlatılır.</exception>
    public static InviteCode Create(string rawCode)
    {
        if (string.IsNullOrWhiteSpace(rawCode))
        {
            throw new InvalidInviteCodeException("Invite code cannot be empty.");
        }

        var normalized = rawCode.Trim().ToUpperInvariant();

        if (normalized.Length != Length)
        {
            throw new InvalidInviteCodeException($"Invite code must be exactly {Length} characters.");
        }

        if (!InviteCodeFormatRegex().IsMatch(normalized))
        {
            throw new InvalidInviteCodeException("Invite code contains invalid characters.");
        }

        return new InviteCode(normalized);
    }

    /// <summary>
    /// Kurallara uygun rastgele yeni bir davet kodu üretir.
    /// </summary>
    /// <returns>Üretilen <see cref="InviteCode"/> değeri.</returns>
    public static InviteCode Generate()
    {
        var code = new string(
            Enumerable.Range(0, Length)
                .Select(_ => AllowedCharacters[Random.Shared.Next(AllowedCharacters.Length)])
                .ToArray());

        return new InviteCode(code);
    }

    /// <summary>
    /// Davet kodu formatını doğrulamak için kullanılan regex desenini döndürür.
    /// </summary>
    /// <returns>Davet kodu format regex'i.</returns>
    [GeneratedRegex(@"^[A-Z2-9]+$", RegexOptions.CultureInvariant)]
    private static partial Regex InviteCodeFormatRegex();
}
