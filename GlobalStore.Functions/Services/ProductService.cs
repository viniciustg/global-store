using GlobalStore.Functions.Models;
using GlobalStore.Functions.Repositories;

namespace GlobalStore.Functions.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public Task<Product?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<string> GetProductsJsonAsync() => _repo.GetProductsJsonAsync();
        public Task CreateAsync(Product product) => _repo.InsertUsingProcedureAsync(product);
        public Task UpdateAsync(Product product) => _repo.UpdateAsync(product);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}
