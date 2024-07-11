using Microsoft.EntityFrameworkCore;
using JobPortal.API.Extensions;
using JobPortal.Application.Extensions;
using JobPortal.Domain.Entities.User;
using JobPortal.Infrastructure.Authentication.Services;
using JobPortal.Persistence;
using JobPortal.Persistence.Configurations;
using JobPortal.Persistence.Extensions;
using Microsoft.AspNetCore.Identity;
using JobPortal.Application.Contracts.Infrastructure;
using System.Runtime.Loader;

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
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddPresentation(builder.Configuration);

            builder.Services.AddTransient<ITokenService, TokenService>();
            builder.Services.AddTransient<IAuthService, AuthService>();

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

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<IdentityContext>();
                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<Role>>();

                await context.Database.MigrateAsync();
                await RoleConfiguration.SeedRolesAndAdmin(userManager, roleManager);

                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("Database migration and seeding completed successfully.");
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred during migration or seeding");
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}