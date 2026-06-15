using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.Users;

/// <summary>
/// Hash'lenmiş kullanıcı şifresini temsil eden value object.
/// </summary>
public sealed class HashedPassword : ValueObject<string>
{
    /// <summary>
    /// Hash'lenmiş şifre değeri.
    /// </summary>
    public override string Value { get; }

    /// <summary>
    /// Doğrulanmış hash değeriyle yeni bir <see cref="HashedPassword"/> örneği oluşturur.
    /// </summary>
    /// <param name="value">Hash'lenmiş şifre metni.</param>
    private HashedPassword(string value) => Value = value;

    /// <summary>
    /// Ham hash metninden geçerli bir <see cref="HashedPassword"/> value object oluşturur.
    /// </summary>
    /// <param name="hash">Hash'lenmiş şifre metni.</param>
    /// <returns>Oluşturulan <see cref="HashedPassword"/> değeri.</returns>
    /// <exception cref="InvalidPasswordHashException">Hash boş veya geçersizse fırlatılır.</exception>
    public static HashedPassword Create(string hash)
    {
        if (string.IsNullOrWhiteSpace(hash))
        {
            throw new InvalidPasswordHashException("Password hash cannot be empty.");
        }

        return new HashedPassword(hash);
    }
}
