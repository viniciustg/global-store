using GlobalStore.Functions.Repositories;
using GlobalStore.Functions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GlobalStore.Functions.Configurations
{
    public static class DependencyInjection
    {
        public static void AddAppDependencies(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
