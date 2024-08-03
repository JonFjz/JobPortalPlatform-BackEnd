using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Infrastructure.Authentication.Services;
using JobPortal.Infrastructure.Cache;
using JobPortal.Infrastructure.Configurations;
using JobPortal.Infrastructure.Email;
using JobPortal.Infrastructure.Network;
using JobPortal.Infrastructure.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobPortal.Infrastructure.Extensions
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConnectionString = configuration.GetSection("Redis:ConnectionString").Value;

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
                options.InstanceName = "JPPInstance";
            });


            services.Configure<BlobStorageSettings>(configuration.GetSection("AzureBlobStorage"));
            services.Configure<Auth0Settings>(configuration.GetSection("Auth0"));
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));




            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IClaimsPrincipalAccessor, ClaimsPrincipalAccessor>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IAuth0Service, Auth0Service>();
            services.AddSingleton<IBlobStorageService, BlobStorageService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddSingleton<ICacheService, CacheService>();

            return services;
        }
    }
}
