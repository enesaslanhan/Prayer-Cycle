using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrayerCycle.Domain.FamilyMembers;
using PrayerCycle.Domain.PrayerLogs;

namespace PrayerCycle.Persistence.PrayerLogs;

internal sealed class PrayerLogConfiguration : IEntityTypeConfiguration<PrayerLog>
{
    public void Configure(EntityTypeBuilder<PrayerLog> builder)
    {
        builder.ToTable("PrayerLogs");

        builder.HasKey(prayerLog => prayerLog.Id);

        builder.Property(prayerLog => prayerLog.Id)
            .HasConversion(id => id.Value, value => PrayerLogId.From(value))
            .ValueGeneratedNever();

        builder.Property(prayerLog => prayerLog.FamilyMemberId)
            .HasConversion(id => id.Value, value => FamilyMemberId.From(value))
            .IsRequired();

        builder.HasOne<FamilyMember>()
            .WithMany()
            .HasForeignKey(prayerLog => prayerLog.FamilyMemberId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(prayerLog => prayerLog.Date)
            .IsRequired();

        builder.Property(prayerLog => prayerLog.PrayerTime)
            .HasConversion<string>()
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(prayerLog => prayerLog.Status)
            .HasConversion<string>()
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(prayerLog => prayerLog.Type)
            .HasConversion<string>()
            .HasMaxLength(16);

        builder.Property(prayerLog => prayerLog.IsQada)
            .IsRequired();

        builder.Property(prayerLog => prayerLog.Notes)
            .HasConversion(
                notes => notes != null ? notes.Value : null,
                value => value != null ? PrayerNotes.Create(value) : null)
            .HasMaxLength(PrayerNotes.MaxLength);

        builder.Property(prayerLog => prayerLog.LoggedAt)
            .IsRequired();

        builder.HasIndex(prayerLog => new { prayerLog.FamilyMemberId, prayerLog.Date });

        builder.Ignore(prayerLog => prayerLog.DomainEvents);
    }
}
