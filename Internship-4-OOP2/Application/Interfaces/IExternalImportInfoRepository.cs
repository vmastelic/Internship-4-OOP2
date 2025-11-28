using Internship_4_OOP2.Domain.Entities;

namespace Application.Interfaces
{
    public interface IExternalImportInfoRepository
    {
        Task<ExternalImportInfo?> GetAsync();
        Task SaveAsync(ExternalImportInfo info);
    }
}
