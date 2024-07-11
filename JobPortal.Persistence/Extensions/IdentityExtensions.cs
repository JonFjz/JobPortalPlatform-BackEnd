using System.Text;
using JobPortal.Domain.Entities.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace JobPortal.Persistence.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
            IConfiguration config)
        {


            services
               .AddIdentityCore<User>(opt =>
               {
                   opt.Password.RequireDigit = true;
                   opt.Password.RequiredLength = 8;
                   opt.Password.RequireNonAlphanumeric = true;
                   opt.Password.RequireUppercase = true;
                   opt.Password.RequireLowercase = true;


                   opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                   opt.Lockout.MaxFailedAccessAttempts = 5;
                   opt.Lockout.AllowedForNewUsers = true;

                   opt.User.RequireUniqueEmail = true;
               })
               .AddRoles<Role>()
               .AddRoleManager<RoleManager<Role>>()
               .AddEntityFrameworkStores<IdentityContext>()
               .AddSignInManager<SignInManager<User>>()
               .AddRoleValidator<RoleValidator<Role>>()
               .AddEntityFrameworkStores<DataContext>();



            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var tokenKey = config["Jwt:TokenKey"] ?? throw new Exception("TokenKey not found");
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddAuthorizationBuilder()
           .AddPolicy("Admin", policy => policy.RequireRole("Admin"));

            return services;
        }
    }
}
