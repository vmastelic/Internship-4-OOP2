using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Internship_4_OOP2.Domain.ValueObjects;

namespace Internship_4_OOP2.Infrastructure.Persistence.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Username)
                .HasColumnName("username")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasConversion(
                     e => e.Value,
                     v => new Email(v)
                 )
                .HasColumnName("email")
                .IsRequired();

            builder.Property(x => x.Password)
                .HasColumnName("password")
                .IsRequired();

            builder.Property(x => x.Website)
                .HasConversion(
                    w => w != null ? w.Value : null,
                    v => v != null ? new Website(v) : null
                )
                .HasColumnName("website")
                .HasMaxLength(100);

            builder.Property(x => x.AddressStreet)
                .HasColumnName("address_street")
                .HasMaxLength(150);

            builder.Property(x => x.AddressCity)
                .HasColumnName("address_city")
                .HasMaxLength(100);

            builder.OwnsOne(x => x.Location, loc =>
            {
                loc.Property(l => l.Lat).HasColumnName("geo_lat");
                loc.Property(l => l.Lng).HasColumnName("geo_lng");
            });

            builder.Property(x => x.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValue(true);

            builder.Property(x => x.CreatedAt).HasColumnName("created_at");
            builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");

            builder.HasIndex(x => x.Username).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
