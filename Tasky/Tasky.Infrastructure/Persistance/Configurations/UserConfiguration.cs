using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasky.Domain.Entities;

namespace Tasky.Infrastructure.Persistance.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(user => user.UserId);
        builder.Property(user => user.UserId).ValueGeneratedNever();

        builder.Property(user => user.UserName)
            .HasMaxLength(UserEntity.UserNameMaxLength)
            .IsRequired();

        builder.Property(user => user.Email)
            .HasMaxLength(UserEntity.EmailMaxLength)
            .IsRequired();

        builder.HasIndex(user => user.Email).IsUnique();

        builder.Property(user => user.PasswordHash)
            .HasMaxLength(256)
            .IsRequired();

        builder.OwnsOne(user => user.Address, address =>
        {
            address.Property(a => a.Street).HasMaxLength(Address.StreetMaxLength);
            address.Property(a => a.City).HasMaxLength(Address.CityMaxLength);
            address.Property(a => a.PostalCode).HasMaxLength(Address.PostalCodeMaxLength);
            address.Property(a => a.Country).HasMaxLength(Address.CountryMaxLength);
        });
    }
}
