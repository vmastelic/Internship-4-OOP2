using Application.Interfaces;
using Dapper;
using Internship_4_OOP2.Domain.Entities;
using Internship_4_OOP2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Internship_4_OOP2.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly CompaniesDbContext _context;
        private readonly IDbConnection _connection;

        public CompanyRepository(CompaniesDbContext context, IDbConnection connection)
        {
            _context = context;
            _connection = connection;
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _connection.QueryAsync<Company>("SELECT * FROM Companies");
        }

        public async Task<Company?> GetByIdAsync(int id)
        {
            return await _connection.QueryFirstOrDefaultAsync<Company>(
                "SELECT * FROM Companies WHERE Id = @id", new { id });
        }

        public async Task AddAsync(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Company company)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var c = await _context.Companies.FindAsync(id);
            if (c != null)
            {
                _context.Companies.Remove(c);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> ExistsByNameAsync(string name)
            => await _context.Companies.AnyAsync(c => c.Name == name);
    }
}
