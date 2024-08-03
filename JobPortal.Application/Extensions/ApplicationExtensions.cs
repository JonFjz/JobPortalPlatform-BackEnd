using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace JobPortal.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
         
            return services;
        }
    }
}
