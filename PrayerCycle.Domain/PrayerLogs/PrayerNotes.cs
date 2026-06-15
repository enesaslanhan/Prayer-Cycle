using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.PrayerLogs;

/// <summary>
/// Namaz kaydına ait opsiyonel not metnini temsil eden value object.
/// </summary>
public sealed class PrayerNotes : ValueObject<string>
{
    /// <summary>
    /// Not metni için maksimum karakter uzunluğu.
    /// </summary>
    public const int MaxLength = 500;

    /// <summary>
    /// Doğrulanmış not metni.
    /// </summary>
    public override string Value { get; }

    /// <summary>
    /// Doğrulanmış not değeriyle yeni bir <see cref="PrayerNotes"/> örneği oluşturur.
    /// </summary>
    /// <param name="value">Trimlenmiş not metni.</param>
    private PrayerNotes(string value) => Value = value;

    /// <summary>
    /// Ham metinden geçerli bir <see cref="PrayerNotes"/> value object oluşturur.
    /// </summary>
    /// <param name="rawNotes">Kullanıcıdan alınan not metni.</param>
    /// <returns>Oluşturulan <see cref="PrayerNotes"/> değeri.</returns>
    /// <exception cref="InvalidPrayerNotesException">Not metni geçersizse fırlatılır.</exception>
    public static PrayerNotes Create(string rawNotes)
    {
        if (string.IsNullOrWhiteSpace(rawNotes))
        {
            throw new InvalidPrayerNotesException("Prayer notes cannot be empty.");
        }

        var trimmed = rawNotes.Trim();

        if (trimmed.Length > MaxLength)
        {
            throw new InvalidPrayerNotesException($"Prayer notes cannot exceed {MaxLength} characters.");
        }

        return new PrayerNotes(trimmed);
    }
}
