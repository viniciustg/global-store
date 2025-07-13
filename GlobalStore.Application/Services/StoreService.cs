using GlobalStore.Domain.Entities;
using GlobalStore.Domain.Interfaces;

namespace GlobalStore.Application.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _repository;

        public StoreService(IStoreRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Store>> GetStoresAsync(Guid companyId) =>
            _repository.GetAllAsync(companyId);

        public Task<Store?> GetStoreAsync(Guid companyId, Guid storeId) =>
            _repository.GetByIdAsync(companyId, storeId);

        public Task<Store> CreateAsync(Store store) =>
            _repository.AddAsync(store);

        public Task<Store> UpdateAsync(Store store) =>
            _repository.UpdateAsync(store);

        public Task DeleteAsync(Guid companyId, Guid storeId) =>
            _repository.DeleteAsync(companyId, storeId);
    }
}
