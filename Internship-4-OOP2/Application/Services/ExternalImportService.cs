using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Internship_4_OOP2.Application.DTOs.External;
using Internship_4_OOP2.Domain.Entities;
using Internship_4_OOP2.Domain.ValueObjects;

namespace Internship_4_OOP2.Application.Services
{
    public class ExternalImportService
    {
        private readonly IExternalUserService _externalUserService;
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IExternalImportInfoRepository _externalImportInfoRepository;

        public ExternalImportService(
            IExternalUserService externalUserService,
            IUserRepository userRepository,
            ICompanyRepository companyRepository,
            IExternalImportInfoRepository externalImportInfoRepository)
        {
            _externalUserService = externalUserService;
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _externalImportInfoRepository = externalImportInfoRepository;
        }

        public async Task ImportExternalUsersAsync()
        {
            var info = await _externalImportInfoRepository.GetAsync();
            if (info != null && info.IsCacheValid())
                return;

            IEnumerable<ExternalUserDto> externalUsers;

            try
            {
                externalUsers = await _externalUserService.GetExternalUsersAsync();
            }
            catch (Exception)
            {
                throw new ValidationException(
                    "External API is not available.",
                    "EXT_API_UNAVAILABLE",
                    Severity.Error);
            }

            foreach (var ext in externalUsers)
            {
                if (await _userRepository.ExistsByEmailAsync(ext.Email) ||
                    await _userRepository.ExistsByUsernameAsync(ext.Username))
                {
                    continue; 
                }

                var email = new Email(ext.Email);
                var website = new Website(ext.Website);
                var location = new GeoLocation(ext.Lat, ext.Lng);

                var password = Guid.NewGuid().ToString();

                Company? company = null;
                if (!string.IsNullOrWhiteSpace(ext.CompanyName))
                {
                    if (!await _companyRepository.ExistsByNameAsync(ext.CompanyName))
                    {
                        company = new Company(ext.CompanyName);
                        await _companyRepository.AddAsync(company);
                    }
                }

                var user = new User(
                    ext.Name,
                    ext.Username,
                    email,
                    ext.Street,
                    ext.City,
                    location,
                    website,
                    password);

                await _userRepository.AddAsync(user);
            }

            if (info == null)
            {
                info = new ExternalImportInfo(DateTime.UtcNow);
            }
            else
            {
                info.RefreshTimestamp();
            }

            await _externalImportInfoRepository.SaveAsync(info);
        }
    }
}
