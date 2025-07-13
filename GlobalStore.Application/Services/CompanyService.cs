using GlobalStore.Domain.Entities;
using GlobalStore.Domain.Interfaces.Repositories;
using GlobalStore.Domain.Interfaces.Services;

namespace GlobalStore.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;

        public CompanyService(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Company>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Company?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);
        public Task<Company> AddAsync(Company company) => _repository.AddAsync(company);
        public Task<Company> UpdateAsync(Company company) => _repository.UpdateAsync(company);
        public Task DeleteAsync(Guid id) => _repository.DeleteAsync(id);
    }
}
