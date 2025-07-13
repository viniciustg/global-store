using GlobalStore.Api.Requests;
using GlobalStore.Api.Responses;
using GlobalStore.Domain.Entities;

namespace GlobalStore.Api.Mappers
{
    public static class StoreMapper
    {
        public static StoreResponse ToResponse(this Store store)
        {
            return new StoreResponse
            {
                Id = store.Id,
                Name = store.Name,
                Address = store.Address,
                CompanyId = store.CompanyId
            };
        }

        public static Store ToEntity(this StoreRequest request, Guid companyId, Guid? storeId = null)
        {
            return new Store
            {
                Id = storeId ?? Guid.NewGuid(),
                Name = request.Name,
                Address = request.Address,
                CompanyId = companyId
            };
        }
    }
}
