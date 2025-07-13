using GlobalStore.Functions.Models;

namespace GlobalStore.Functions.Services
{
    public interface IProductService
    {
        Task<Product?> GetByIdAsync(int id);
        Task<string> GetProductsJsonAsync();
        Task CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
    }
}
