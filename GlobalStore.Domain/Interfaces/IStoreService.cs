using GlobalStore.Domain.Entities;

namespace GlobalStore.Domain.Interfaces
{
    public interface IStoreService
    {
        Task<IEnumerable<Store>> GetStoresAsync(Guid companyId);
        Task<Store?> GetStoreAsync(Guid companyId, Guid storeId);
        Task<Store> CreateAsync(Store store);
        Task<Store> UpdateAsync(Store store);
        Task DeleteAsync(Guid companyId, Guid storeId);
    }
}
