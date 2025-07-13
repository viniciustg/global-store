using GlobalStore.Domain.Entities;

namespace GlobalStore.Domain.Interfaces
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Store>> GetAllAsync(Guid companyId);
        Task<Store?> GetByIdAsync(Guid companyId, Guid storeId);
        Task<Store> AddAsync(Store store);
        Task<Store> UpdateAsync(Store store);
        Task DeleteAsync(Guid companyId, Guid storeId);
    }
}
