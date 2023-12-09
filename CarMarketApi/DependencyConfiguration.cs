using CarMarketApi.Repository;
using CarMarketApi.Repository.AllRepositories;
using CarMarketApi.Repository.RepositoryAbstracts;
using CarMarketApi.Service.AllServices;
using CarMarketApi.Service.ServiceAbstracts;

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
            services.AddScoped<ISellerService, SellerServices>();
            services.AddScoped<IBuyerService, BuyerServices>();
            services.AddScoped<IItemService, ItemService>();

            return services;
        }
    }
}
