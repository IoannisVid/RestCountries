using RestCountries.Core.Settings;
using RestCountries.Infrastructure.Repository;

namespace RestCountries.Infrastructure.Configuration
{
    public static class DefaultConfiguration
    {
        public static IServiceCollection AddDefaultConfiguration(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();
            services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = configuration!.GetConnectionString("connectionString");
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Configure<HttpClientSettings>(configuration!.GetSection(nameof(HttpClientSettings)));
            services.Configure<ApiSettings>(configuration!.GetSection(nameof(ApiSettings)));

            return services;
        }
    }
}
