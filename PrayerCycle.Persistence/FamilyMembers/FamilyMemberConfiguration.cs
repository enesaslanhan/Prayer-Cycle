using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrayerCycle.Domain.Families;
using PrayerCycle.Domain.FamilyMembers;
using PrayerCycle.Domain.Users;

namespace PrayerCycle.Persistence.FamilyMembers;

internal sealed class FamilyMemberConfiguration : IEntityTypeConfiguration<FamilyMember>
{
    public void Configure(EntityTypeBuilder<FamilyMember> builder)
    {
        builder.ToTable("FamilyMembers");

        builder.HasKey(member => member.Id);

        builder.Property(member => member.Id)
            .HasConversion(id => id.Value, value => FamilyMemberId.From(value))
            .ValueGeneratedNever();

        builder.Property(member => member.FamilyId)
            .HasConversion(id => id.Value, value => FamilyId.From(value))
            .IsRequired();

        builder.HasOne<Family>()
            .WithMany()
            .HasForeignKey(member => member.FamilyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(member => member.UserId)
            .HasConversion(
                id => id.HasValue ? id.Value.Value : (Guid?)null,
                value => value.HasValue ? UserId.From(value.Value) : null);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(member => member.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(member => member.Name)
            .HasConversion(name => name.Value, value => MemberName.Create(value))
            .HasMaxLength(MemberName.MaxLength)
            .IsRequired();

        builder.Property(member => member.Role)
            .HasConversion<string>()
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(member => member.AvatarColor)
            .HasConversion(color => color.Value, value => AvatarColor.Create(value))
            .HasMaxLength(7)
            .IsRequired();

        builder.Property(member => member.AvatarInitial)
            .HasConversion(
                initial => initial != null ? initial.Value : null,
                value => value != null ? AvatarInitial.Create(value) : null)
            .HasMaxLength(1);

        builder.Property(member => member.BirthDate);

        builder.Property(member => member.IsActive)
            .IsRequired();

        builder.Property(member => member.CreatedAt)
            .IsRequired();

        builder.Ignore(member => member.DomainEvents);
    }
}
