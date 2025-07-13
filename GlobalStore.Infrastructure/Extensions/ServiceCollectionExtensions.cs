using GlobalStore.Application.Services;
using GlobalStore.Domain.Interfaces;
using GlobalStore.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GlobalStore.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IStoreService, StoreService>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IStoreRepository, StoreRepository>();
            return services;
        }
    }
}
