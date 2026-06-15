using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrayerCycle.Domain.Users;

namespace PrayerCycle.Persistence.Users;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
            .HasConversion(id => id.Value, value => UserId.From(value))
            .ValueGeneratedNever();

        builder.Property(user => user.Email)
            .HasConversion(email => email.Value, value => Email.Create(value))
            .HasMaxLength(256)
            .IsRequired();

        builder.HasIndex(user => user.Email)
            .IsUnique();

        builder.Property(user => user.PasswordHash)
            .HasConversion(
                password => password != null ? password.Value : null,
                value => value != null ? HashedPassword.Create(value) : null)
            .HasMaxLength(512);

        builder.Property(user => user.DisplayName)
            .HasConversion(name => name.Value, value => DisplayName.Create(value))
            .HasMaxLength(DisplayName.MaxLength)
            .IsRequired();

        builder.Property(user => user.CreatedAt)
            .IsRequired();

        builder.Property(user => user.LastLoginAt);

        builder.Property(user => user.IsActive)
            .IsRequired();

        builder.Ignore(user => user.DomainEvents);
    }
}
