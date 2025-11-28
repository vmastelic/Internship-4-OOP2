using Internship_4_OOP2.Domain.Entities;

namespace Application.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company?> GetByIdAsync(int id);
        Task AddAsync(Company company);
        Task UpdateAsync(Company company);
        Task DeleteAsync(int id);
        Task<bool> ExistsByNameAsync(string name);
    }
}
