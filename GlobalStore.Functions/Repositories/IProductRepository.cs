using GlobalStore.Functions.Models;

namespace GlobalStore.Functions.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task<string> GetProductsJsonAsync();
        Task InsertUsingProcedureAsync(Product product);
        Task InsertAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
    }
}