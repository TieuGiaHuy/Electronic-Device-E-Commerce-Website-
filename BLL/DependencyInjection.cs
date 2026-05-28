using DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class DependencyInjection
    {
        public static IServiceProvider AddBusinessLogicLayer(this IServiceCollection services, string connectionString)
        {
            services.AddDataAccessLayer(connectionString);
            // Register business logic services here
            // e.g., services.AddScoped<IProductService, ProductService>();
            return services.BuildServiceProvider();
        }
    }
}
