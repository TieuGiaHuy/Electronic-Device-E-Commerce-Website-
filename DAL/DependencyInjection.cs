using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class DependencyInjection
    {
        public static IServiceProvider AddDataAccessLayer(this IServiceCollection services, string connectionString)
        {
            // Register the DbContext with the connection string from configuration
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString)
                .UseSnakeCaseNamingConvention()); // Sử dụng SnakeCaseNamingConvention để tự động chuyển đổi tên bảng và cột sang snake_case
            // Register repositories or other services here if needed
            // e.g., services.AddScoped<IProductRepository, ProductRepository>();
            return services.BuildServiceProvider();
        }
    }
}
