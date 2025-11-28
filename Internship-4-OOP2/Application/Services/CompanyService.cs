using Application.DTOs.Companies;
using Application.Interfaces;
using Domain.Enums;
using Domain.Exceptions;
using Internship_4_OOP2.Domain.Entities;

namespace Internship_4_OOP2.Application.Services
{
    public class CompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;

        public CompanyService(ICompanyRepository companyRepository, IUserRepository userRepository)
        {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
        }
        private async Task ValidateUserAccess(string username, string password)
        {
            var user = await _userRepository.GetByCredentialsAsync(username, password);

            if (user == null)
                throw new ValidationException(
                    "Invalid credentials.",
                    "AUTH_INVALID_CREDENTIALS",
                    Severity.Error);

            if (!user.IsActive)
                throw new ValidationException(
                    "User is deactivated.",
                    "AUTH_USER_INACTIVE",
                    Severity.Error);
        }

        public async Task<IEnumerable<CompanyResponseDto>> GetAllAsync(string username, string password)
        {
            await ValidateUserAccess(username, password);

            var companies = await _companyRepository.GetAllAsync();

            return companies.Select(c => new CompanyResponseDto
            {
                Id = c.Id,
                Name = c.Name
            });
        }

        public async Task<CompanyResponseDto?> GetByIdAsync(int id, string username, string password)
        {
            await ValidateUserAccess(username, password);

            var company = await _companyRepository.GetByIdAsync(id);
            if (company == null) return null;

            return new CompanyResponseDto
            {
                Id = company.Id,
                Name = company.Name
            };
        }

        public async Task<int> CreateAsync(CreateCompanyDto dto, string username, string password)
        {
            await ValidateUserAccess(username, password);

            if (await _companyRepository.ExistsByNameAsync(dto.Name))
                throw new ValidationException("Company name must be unique.", "CMP_NAME_EXISTS", Severity.Error);

            var company = new Company(dto.Name);
            await _companyRepository.AddAsync(company);

            return company.Id;
        }

        public async Task UpdateAsync(int id, UpdateCompanyDto dto, string username, string password)
        {
            await ValidateUserAccess(username, password);

            var company = await _companyRepository.GetByIdAsync(id);
            if (company == null)
                throw new ValidationException("Company not found.", "CMP_NOT_FOUND", Severity.Error);

            if (await _companyRepository.ExistsByNameAsync(dto.Name))
                throw new ValidationException("Company name must be unique.", "CMP_NAME_EXISTS", Severity.Error);

            company.Update(dto.Name);
            await _companyRepository.UpdateAsync(company);
        }

        public async Task DeleteAsync(int id, string username, string password)
        {
            await ValidateUserAccess(username, password);
            await _companyRepository.DeleteAsync(id);
        }
    }
}
