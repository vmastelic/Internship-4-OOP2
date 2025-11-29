using Domain.Entities;
using Internship_4_OOP2.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Internship_4_OOP2.Infrastructure.Persistence.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(100);
            builder.Property(x => x.Username).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Email).HasConversion(x => x.Value, v => new Email(v)).IsRequired();
            builder.Property(x => x.AddressStreet).HasMaxLength(150);
            builder.Property(x => x.AddressCity).HasMaxLength(100);

            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.IsActive).HasDefaultValue(true);

            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.Username).IsUnique();
        }
    }


}
