using BuyEmAll.Core.Configs;
using BuyEmAll.Core.Interfaces;
using BuyEmAll.Core.Interfaces.Services;
using BuyEmAll.Infrastructure.Data;
using BuyEmAll.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace BuyEmAll.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration Configuration)
        {
            // Repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // This is specified with typeof because 
            // its generic value could be anything

            // Services
            services.AddScoped<ITokenService, TokenService>();


            // Configurations
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddSingleton<IConnectionMultiplexer>(c => {
                var configuration = ConfigurationOptions.Parse(Configuration.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });

            return services;
        }
    }
}