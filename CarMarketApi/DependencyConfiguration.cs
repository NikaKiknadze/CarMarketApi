using CarMarketApi.Repository.AllRepositories;
using CarMarketApi.Repository;
using CarMarketApi.Repository.RepositoryAbstracts;

namespace CarMarketApi
{
    public static class DependencyConfiguration
    {
        public static IServiceCollection RegisterDependencyConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IRepositories, Repositories>();


            services.AddScoped<ISellerRepository, SellerRepository>();
            services.AddScoped<IBuyerRepository, BuyerRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();


            return services;
        }
    }
}
