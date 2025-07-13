using GlobalStore.Api.Requests;
using GlobalStore.Api.Responses;
using GlobalStore.Domain.Entities;

namespace GlobalStore.Api.Mappers
{
    public static class CompanyMapper
    {
        public static CompanyResponse ToResponse(this Company company) =>
            new()
            { 
                Id = company.Id, 
                Name = company.Name 
            };

        public static Company ToEntity(this CompanyRequest request, Guid? id = null) =>
            new()
            { 
                Id = id ?? Guid.NewGuid(), 
                Name = request.Name 
            };
    }
}
