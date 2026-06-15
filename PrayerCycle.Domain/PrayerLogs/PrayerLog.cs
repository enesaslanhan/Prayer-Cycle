using PrayerCycle.Domain.Common;
using PrayerCycle.Domain.FamilyMembers;

namespace PrayerCycle.Domain.PrayerLogs;

/// <summary>
/// Aile üyesine ait namaz kaydını temsil eden aggregate root.
/// </summary>
public sealed class PrayerLog : AggregateRoot<PrayerLogId>
{
    /// <summary>
    /// Kaydın ait olduğu aile üyesinin kimliği.
    /// </summary>
    public FamilyMemberId FamilyMemberId { get; private set; }

    /// <summary>
    /// Namazın kaydedildiği gün.
    /// </summary>
    public DateOnly Date { get; private set; }

    /// <summary>
    /// Namaz vakti.
    /// </summary>
    public PrayerTime PrayerTime { get; private set; }

    /// <summary>
    /// Namaz durumu.
    /// </summary>
    public PrayerStatus Status { get; private set; }

    /// <summary>
    /// Kılınan namazın türü. Kaçırılan namazlarda null olabilir.
    /// </summary>
    public PrayerType? Type { get; private set; }

    /// <summary>
    /// Kaydın kaza namazına ait olup olmadığını belirtir.
    /// </summary>
    public bool IsQada { get; private set; }

    /// <summary>
    /// Kayda ait opsiyonel not.
    /// </summary>
    public PrayerNotes? Notes { get; private set; }

    /// <summary>
    /// Kaydın oluşturulma veya son güncellenme zamanı (UTC).
    /// </summary>
    public DateTime LoggedAt { get; private set; }

    /// <summary>
    /// ORM ve serileştirme için gerekli olan parametresiz kurucu.
    /// </summary>
    private PrayerLog()
    {
    }

    /// <summary>
    /// Yeni bir namaz kaydı oluşturur.
    /// </summary>
    /// <param name="familyMemberId">Aile üyesi kimliği.</param>
    /// <param name="date">Namaz günü.</param>
    /// <param name="prayerTime">Namaz vakti.</param>
    /// <param name="status">Namaz durumu.</param>
    /// <param name="type">Namaz türü. Kılınan namazlar için belirtilebilir.</param>
    /// <param name="isQada">Kaza namazı olup olmadığı.</param>
    /// <param name="notes">Opsiyonel not.</param>
    /// <returns>Oluşturulan namaz kaydı aggregate'i.</returns>
    /// <exception cref="InvalidPrayerLogException">İş kuralları ihlal edilirse fırlatılır.</exception>
    public static PrayerLog Create(
        FamilyMemberId familyMemberId,
        DateOnly date,
        PrayerTime prayerTime,
        PrayerStatus status,
        PrayerType? type = null,
        bool isQada = false,
        PrayerNotes? notes = null)
    {
        ValidateDate(date);
        ValidateStatusAndType(status, type);

        var prayerLog = new PrayerLog
        {
            Id = PrayerLogId.New(),
            FamilyMemberId = familyMemberId,
            Date = date,
            PrayerTime = prayerTime,
            Status = status,
            Type = status == PrayerStatus.Performed ? type : null,
            IsQada = isQada,
            Notes = notes,
            LoggedAt = DateTime.UtcNow
        };

        prayerLog.RaiseDomainEvent(new PrayerLogCreated(
            prayerLog.Id,
            familyMemberId,
            date,
            prayerTime,
            status));

        return prayerLog;
    }

    /// <summary>
    /// Namaz durumunu ve türünü günceller.
    /// </summary>
    /// <param name="status">Yeni namaz durumu.</param>
    /// <param name="type">Yeni namaz türü. Kılınan namazlar için belirtilebilir.</param>
    /// <exception cref="InvalidPrayerLogException">İş kuralları ihlal edilirse fırlatılır.</exception>
    public void UpdateStatus(PrayerStatus status, PrayerType? type = null)
    {
        ValidateStatusAndType(status, type);

        Status = status;
        Type = status == PrayerStatus.Performed ? type : null;
        LoggedAt = DateTime.UtcNow;

        RaiseDomainEvent(new PrayerLogUpdated(Id, Status, Type, IsQada));
    }

    /// <summary>
    /// Kaydın kaza namazı bilgisini günceller.
    /// </summary>
    /// <param name="isQada">Kaza namazı olup olmadığı.</param>
    public void SetQada(bool isQada)
    {
        IsQada = isQada;
        LoggedAt = DateTime.UtcNow;

        RaiseDomainEvent(new PrayerLogUpdated(Id, Status, Type, IsQada));
    }

    /// <summary>
    /// Kayda ait notu günceller.
    /// </summary>
    /// <param name="notes">Yeni not. Null verilirse not temizlenir.</param>
    public void UpdateNotes(PrayerNotes? notes)
    {
        Notes = notes;
        LoggedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Namaz gününün gelecekte olmadığını doğrular.
    /// </summary>
    /// <param name="date">Doğrulanacak gün.</param>
    /// <exception cref="InvalidPrayerLogException">Tarih gelecekteyse fırlatılır.</exception>
    private static void ValidateDate(DateOnly date)
    {
        if (date > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new InvalidPrayerLogException("Prayer log date cannot be in the future.");
        }
    }

    /// <summary>
    /// Namaz durumu ile tür bilgisinin tutarlı olduğunu doğrular.
    /// </summary>
    /// <param name="status">Namaz durumu.</param>
    /// <param name="type">Namaz türü.</param>
    /// <exception cref="InvalidPrayerLogException">Durum ve tür tutarsızsa fırlatılır.</exception>
    private static void ValidateStatusAndType(PrayerStatus status, PrayerType? type)
    {
        if (status == PrayerStatus.Missed && type is not null)
        {
            throw new InvalidPrayerLogException("Missed prayers cannot have a prayer type.");
        }
    }
}
