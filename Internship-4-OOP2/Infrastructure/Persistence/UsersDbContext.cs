using Domain.Entities;
using Internship_4_OOP2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Internship_4_OOP2.Infrastructure.Persistence
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<ExternalImportInfo> ExternalImportInfos => Set<ExternalImportInfo>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(UsersDbContext).Assembly);
        
        }
    }
}
