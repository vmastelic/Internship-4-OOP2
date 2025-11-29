using Application.Interfaces;
using Dapper;
using Domain.Entities;
using Internship_4_OOP2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Internship_4_OOP2.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersDbContext _context;
        private readonly IDbConnection _connection; 

        public UserRepository(UsersDbContext context, IDbConnection connection)
        {
            _context = context;
            _connection = connection;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var sql = "SELECT * FROM Users";
            var users = await _connection.QueryAsync<User>(sql);
            return users;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Users WHERE Id = @id";
            return await _connection.QueryFirstOrDefaultAsync<User>(sql, new { id });
        }


        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsByEmailAsync(string email)
            => await _context.Users.AnyAsync(u => u.Email.Value == email);

        public async Task<bool> ExistsByUsernameAsync(string username) 
            => await _context.Users.AnyAsync(u => u.Username == username);

        public async Task<User?> GetByCredentialsAsync(string username, string password)
            => await _context.Users.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
    }
}
