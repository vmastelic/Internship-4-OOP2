using Internship_4_OOP2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Internship_4_OOP2.Infrastructure.Persistence.Configurations
{
    public class ExternalImportInfoConfig : IEntityTypeConfiguration<ExternalImportInfo>
    {
        public void Configure(EntityTypeBuilder<ExternalImportInfo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.LastImportedAt).IsRequired();
        }
    }
}
