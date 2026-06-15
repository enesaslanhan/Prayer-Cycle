using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrayerCycle.Domain.Families;
using PrayerCycle.Domain.Users;

namespace PrayerCycle.Persistence.Families;

internal sealed class FamilyConfiguration : IEntityTypeConfiguration<Family>
{
    public void Configure(EntityTypeBuilder<Family> builder)
    {
        builder.ToTable("Families");

        builder.HasKey(family => family.Id);

        builder.Property(family => family.Id)
            .HasConversion(id => id.Value, value => FamilyId.From(value))
            .ValueGeneratedNever();

        builder.Property(family => family.Name)
            .HasConversion(name => name.Value, value => FamilyName.Create(value))
            .HasMaxLength(FamilyName.MaxLength)
            .IsRequired();

        builder.Property(family => family.OwnerUserId)
            .HasConversion(id => id.Value, value => UserId.From(value))
            .IsRequired();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(family => family.OwnerUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(family => family.CreatedAt)
            .IsRequired();

        builder.Property(family => family.InviteCode)
            .HasConversion(
                code => code != null ? code.Value : null,
                value => value != null ? InviteCode.Create(value) : null)
            .HasMaxLength(InviteCode.Length);

        builder.HasIndex(family => family.InviteCode)
            .IsUnique()
            .HasFilter("[InviteCode] IS NOT NULL");

        builder.Property(family => family.MaxMembers)
            .HasConversion(maxMembers => maxMembers.Value, value => MaxMembers.Create(value))
            .IsRequired();

        builder.Ignore(family => family.DomainEvents);
    }
}
