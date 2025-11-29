using Application.Interfaces;
using Internship_4_OOP2.Domain.Entities;
using Internship_4_OOP2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Internship_4_OOP2.Infrastructure.Repositories
{
    public class ExternalImportInfoRepository : IExternalImportInfoRepository
    {
        private readonly UsersDbContext _context;

        public ExternalImportInfoRepository(UsersDbContext context)
        {
            _context = context;
        }

        public async Task<ExternalImportInfo?> GetAsync()
            => await _context.ExternalImportInfos.FirstOrDefaultAsync();

        public async Task SaveAsync(ExternalImportInfo info)
        {
            if (_context.ExternalImportInfos.Any())
                _context.ExternalImportInfos.Update(info);
            else
                _context.ExternalImportInfos.Add(info);

            await _context.SaveChangesAsync();
        }
    }
}
