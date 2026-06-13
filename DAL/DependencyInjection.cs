using DAL.Data;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
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
            
            // Đăng ký các repository của bạn ở đây
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductVariantRepository, ProductVariantRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();

            return services.BuildServiceProvider();
        }
    }
}
