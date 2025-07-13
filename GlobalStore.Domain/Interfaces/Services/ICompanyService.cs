using GlobalStore.Domain.Entities;

namespace GlobalStore.Domain.Interfaces.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company?> GetByIdAsync(Guid id);
        Task<Company> AddAsync(Company company);
        Task<Company> UpdateAsync(Company company);
        Task DeleteAsync(Guid id);
    }
}
