using PrayerCycle.Domain.Common;

namespace PrayerCycle.Domain.Users;

/// <summary>
/// Kimlik ve giriş işlemlerini yöneten kullanıcı aggregate root'u.
/// </summary>
public sealed class User : AggregateRoot<UserId>
{
    /// <summary>
    /// Kullanıcının e-posta adresi.
    /// </summary>
    public Email Email { get; private set; } = null!;

    /// <summary>
    /// Kullanıcının şifre hash değeri. Google ile giriş yapan kullanıcılarda null olabilir.
    /// </summary>
    public HashedPassword? PasswordHash { get; private set; }

    /// <summary>
    /// Kullanıcının görünen adı.
    /// </summary>
    public DisplayName DisplayName { get; private set; } = null!;

    /// <summary>
    /// Kullanıcının oluşturulma zamanı (UTC).
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Kullanıcının son giriş zamanı (UTC).
    /// </summary>
    public DateTime? LastLoginAt { get; private set; }

    /// <summary>
    /// Kullanıcının aktif olup olmadığını belirtir.
    /// </summary>
    public bool IsActive { get; private set; }

    /// <summary>
    /// ORM ve serileştirme için gerekli olan parametresiz kurucu.
    /// </summary>
    private User()
    {
    }

    /// <summary>
    /// E-posta ve şifre ile yeni bir kullanıcı kaydı oluşturur.
    /// </summary>
    /// <param name="email">Kullanıcı e-posta adresi.</param>
    /// <param name="passwordHash">Hash'lenmiş şifre.</param>
    /// <param name="displayName">Görünen ad.</param>
    /// <returns>Oluşturulan kullanıcı aggregate'i.</returns>
    public static User RegisterWithPassword(Email email, HashedPassword passwordHash, DisplayName displayName)
    {
        var user = new User
        {
            Id = UserId.New(),
            Email = email,
            PasswordHash = passwordHash,
            DisplayName = displayName,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        user.RaiseDomainEvent(new UserRegistered(user.Id, user.Email));

        return user;
    }

    /// <summary>
    /// Google ile giriş yapan yeni bir kullanıcı kaydı oluşturur.
    /// </summary>
    /// <param name="email">Kullanıcı e-posta adresi.</param>
    /// <param name="displayName">Görünen ad.</param>
    /// <returns>Oluşturulan kullanıcı aggregate'i.</returns>
    public static User RegisterWithGoogle(Email email, DisplayName displayName)
    {
        var user = new User
        {
            Id = UserId.New(),
            Email = email,
            PasswordHash = null,
            DisplayName = displayName,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        user.RaiseDomainEvent(new UserRegistered(user.Id, user.Email));

        return user;
    }

    /// <summary>
    /// Kullanıcının giriş yaptığını kaydeder ve son giriş zamanını günceller.
    /// </summary>
    /// <exception cref="UserInactiveException">Kullanıcı aktif değilse fırlatılır.</exception>
    public void RecordLogin()
    {
        EnsureActive();

        var loginAt = DateTime.UtcNow;
        LastLoginAt = loginAt;

        RaiseDomainEvent(new UserLoggedIn(Id, loginAt));
    }

    /// <summary>
    /// Kullanıcıyı pasif duruma alır.
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
    }

    /// <summary>
    /// Pasif kullanıcıyı tekrar aktif duruma alır.
    /// </summary>
    public void Reactivate()
    {
        IsActive = true;
    }

    /// <summary>
    /// Aktif kullanıcının görünen adını günceller.
    /// </summary>
    /// <param name="displayName">Yeni görünen ad.</param>
    /// <exception cref="UserInactiveException">Kullanıcı aktif değilse fırlatılır.</exception>
    public void ChangeDisplayName(DisplayName displayName)
    {
        EnsureActive();
        DisplayName = displayName;
    }

    /// <summary>
    /// Aktif kullanıcının şifre hash değerini günceller.
    /// </summary>
    /// <param name="passwordHash">Yeni hash'lenmiş şifre.</param>
    /// <exception cref="UserInactiveException">Kullanıcı aktif değilse fırlatılır.</exception>
    public void SetPasswordHash(HashedPassword passwordHash)
    {
        EnsureActive();
        PasswordHash = passwordHash;
    }

    /// <summary>
    /// Kullanıcının aktif olduğunu doğrular; değilse exception fırlatır.
    /// </summary>
    /// <exception cref="UserInactiveException">Kullanıcı aktif değilse fırlatılır.</exception>
    private void EnsureActive()
    {
        if (!IsActive)
        {
            throw new UserInactiveException();
        }
    }
}
