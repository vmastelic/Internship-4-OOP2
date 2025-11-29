using Internship_4_OOP2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Internship_4_OOP2.Infrastructure.Persistence
{
    public class CompaniesDbContext : DbContext
    {
        public CompaniesDbContext(DbContextOptions<CompaniesDbContext> options)
            : base(options) { }

        public DbSet<Company> Companies => Set<Company>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(CompaniesDbContext).Assembly);
        }
    }


}
