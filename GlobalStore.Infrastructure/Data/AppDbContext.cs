using GlobalStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GlobalStore.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Store> Stores => Set<Store>();
        public DbSet<Company> Companies => Set<Company>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.HasOne(s => s.Company)
                      .WithMany(c => c.Stores)
                      .HasForeignKey(s => s.CompanyId);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(c => c.Id);
            });
        }
    }
}
