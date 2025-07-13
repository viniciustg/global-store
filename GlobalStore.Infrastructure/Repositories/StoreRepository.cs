using GlobalStore.Domain.Entities;
using GlobalStore.Domain.Interfaces.Repositories;
using GlobalStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GlobalStore.Infrastructure.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly AppDbContext _context;

        public StoreRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Store>> GetAllAsync(Guid companyId)
        {
            return await _context.Stores
                .Where(s => s.CompanyId == companyId)
                .ToListAsync();
        }

        public async Task<Store?> GetByIdAsync(Guid companyId, Guid storeId)
        {
            return await _context.Stores
                .FirstOrDefaultAsync(s => s.CompanyId == companyId && s.Id == storeId);
        }

        public async Task<Store> AddAsync(Store store)
        {
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
            return store;
        }

        public async Task<Store> UpdateAsync(Store store)
        {
            _context.Stores.Update(store);
            await _context.SaveChangesAsync();
            return store;
        }

        public async Task DeleteAsync(Guid companyId, Guid storeId)
        {
            var store = await _context.Stores
                .FirstOrDefaultAsync(s => s.CompanyId == companyId && s.Id == storeId);

            if (store is not null)
            {
                _context.Stores.Remove(store);
                await _context.SaveChangesAsync();
            }
        }
    }
}
