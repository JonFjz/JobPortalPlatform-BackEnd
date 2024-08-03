using JobPortal.Application.Contracts.Persistence;
using JobPortal.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace JobPortal.Persistence.Extensions
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var options = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(options);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
