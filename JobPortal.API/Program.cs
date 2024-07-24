using JobPortal.API.Extensions;
using JobPortal.Application.Extensions;
using JobPortal.Infrastructure.Authentication.Services;
using JobPortal.Persistence.Extensions;
using JobPortal.Infrastructure.Extensions;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Infrastructure.Storage;
using JobPortal.Application.Helpers.Models.Auth0;
using JobPortal.Infrastructure.Network;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Runtime.Loader;
using JobPortal.Infrastructure.Payment;
using JobPortal.Application.Helpers.Models.Stripe;
using Stripe;

namespace JobPortal.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var files = Directory.GetFiles(
                AppDomain.CurrentDomain.BaseDirectory,
                "JobPortal*.dll");

            var assemblies = files
                .Select(p => AssemblyLoadContext.Default.LoadFromAssemblyPath(p));

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpClient();
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddPresentation(builder.Configuration);
            builder.Services.AddCacheServices(builder.Configuration);

            builder.Services.Configure<Auth0Settings>(builder.Configuration.GetSection("Auth0"));

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IClaimsPrincipalAccessor, ClaimsPrincipalAccessor>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IAuth0Service, Auth0Service>();
            builder.Services.AddSingleton<IBlobStorageService, BlobStorageService>();


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = builder.Configuration["Auth0:Authority"];
                options.Audience = builder.Configuration["Auth0:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("JobSeeker", policy =>
                    policy.RequireClaim("https://dev-2si34b7jockzxhln/role", "JobSeeker"));
                options.AddPolicy("Employer", policy =>
                    policy.RequireClaim("https://dev-2si34b7jockzxhln/role", "Employer"));
            });

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("https://localhost:7136")
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddAdvancedDependencyInjection();

            builder.Services.Scan(p => p.FromAssemblies(assemblies)
                .AddClasses()
                .AsMatchingInterface());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
