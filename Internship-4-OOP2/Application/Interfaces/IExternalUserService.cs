using Internship_4_OOP2.Application.DTOs.External;

namespace Application.Interfaces
{
    public interface IExternalUserService
    {
        Task<IEnumerable<ExternalUserDto>> GetExternalUsersAsync();
    }
}
