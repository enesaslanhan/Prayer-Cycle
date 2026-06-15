using System.Net.Mail;
using System.Text.RegularExpressions;
using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.Users;

/// <summary>
/// Geçerli ve normalize edilmiş e-posta adresini temsil eden value object.
/// </summary>
public sealed partial class Email : ValueObject<string>
{
    private const int MaxLength = 256;

    /// <summary>
    /// Normalize edilmiş e-posta adresi.
    /// </summary>
    public override string Value { get; }

    /// <summary>
    /// Doğrulanmış e-posta değeriyle yeni bir <see cref="Email"/> örneği oluşturur.
    /// </summary>
    /// <param name="value">Normalize edilmiş e-posta adresi.</param>
    private Email(string value) => Value = value;

    /// <summary>
    /// Ham e-posta metninden geçerli bir <see cref="Email"/> value object oluşturur.
    /// </summary>
    /// <param name="rawEmail">Kullanıcıdan alınan e-posta metni.</param>
    /// <returns>Oluşturulan <see cref="Email"/> değeri.</returns>
    /// <exception cref="InvalidEmailException">E-posta geçersizse fırlatılır.</exception>
    public static Email Create(string rawEmail)
    {
        if (string.IsNullOrWhiteSpace(rawEmail))
        {
            throw new InvalidEmailException("Email cannot be empty.");
        }

        var normalized = rawEmail.Trim().ToLowerInvariant();

        if (normalized.Length > MaxLength)
        {
            throw new InvalidEmailException($"Email cannot exceed {MaxLength} characters.");
        }

        if (!EmailFormatRegex().IsMatch(normalized))
        {
            throw new InvalidEmailException("Email format is invalid.");
        }

        try
        {
            _ = new MailAddress(normalized);
        }
        catch (FormatException ex)
        {
            throw new InvalidEmailException("Email format is invalid.", ex);
        }

        return new Email(normalized);
    }

    /// <summary>
    /// E-posta formatını doğrulamak için kullanılan regex desenini döndürür.
    /// </summary>
    /// <returns>E-posta format regex'i.</returns>
    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.CultureInvariant)]
    private static partial Regex EmailFormatRegex();
}
