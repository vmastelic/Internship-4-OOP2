using Application.Interfaces;
using Dapper;
using Domain.Entities;
using Internship_4_OOP2.Domain.ValueObjects;
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
            var sql = "SELECT id, name, username, email, address_street, address_city, geo_lat, geo_lng, website, password, created_at, updated_at, is_active FROM users";

            var result = await _connection.QueryAsync(sql);

            return result.Select(x => new User(
                x.id,
                x.name,
                x.username,
                new Email((string)x.email),
                x.address_street,
                x.address_city,
                new GeoLocation((double)x.geo_lat, (double)x.geo_lng),
                x.website != null ? new Website((string)x.website) : null,
                x.password,
                x.created_at,
                x.updated_at,
                x.is_active
            ));
        }


        public async Task<User?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM users WHERE id=@id";
            var result = await _connection.QueryFirstOrDefaultAsync(sql, new { id });

            if (result == null) return null;

            return new User(
                result.id,
                result.name,
                result.username,
                new Email((string)result.email),
                result.address_street,
                result.address_city,
                new GeoLocation((double)result.geo_lat, (double)result.geo_lng),
                result.website != null ? new Website((string)result.website) : null,
                result.password,
                result.created_at,
                result.updated_at,
                result.is_active
            );
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
            => await _context.Users.AnyAsync(u => u.Email == new Email(email));

        public async Task<bool> ExistsByUsernameAsync(string username)
            => await _context.Users.AnyAsync(u => u.Username == username);

        public async Task<User?> GetByCredentialsAsync(string username, string password)
            => await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);


    }
}
