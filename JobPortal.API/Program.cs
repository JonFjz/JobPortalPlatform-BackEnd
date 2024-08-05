using JobPortal.API.Extensions;
using JobPortal.Application.Extensions;
using JobPortal.Persistence.Extensions;
using JobPortal.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Runtime.Loader;
using JobPortal.Application.Contracts.Infrastructure;
using MassTransit;
using JobPortal.Infrastructure.RealTime;
using FluentValidation;
using FluentValidation.AspNetCore;
using JobPortal.Application.Features.Educations.Commands.CreateEducation.Validator;
using JobPortal.Worker.Services;
using JobPortal.Infrastructure.GoogleCalendar;

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

            builder.Services.AddHttpClient();
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddPresentation(builder.Configuration);
            builder.Services.AddInfrastructureServices(builder.Configuration);


            builder.Services.AddSingleton<IGoogleCalendarService, GoogleCalendarService>();
            builder.Services.AddSignalR();
            builder.Services.AddSingleton<IRealTimeService, RealTimeService>();

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<JobPostingExpiredConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("34.141.119.232");
                    cfg.ConfigureEndpoints(context);
                });
            });


            //builder.Services.AddMassTransit(x =>
            //{
            //    x.AddConsumer<JobPostingExpiredConsumer>();
            //    x.UsingRabbitMq((context, cfg) =>
            //    {
            //        cfg.Host("rabbitmq://localhost");
            //        cfg.ConfigureEndpoints(context);
            //    });
            //});


            builder.Services.AddHostedService<JobPostingExpiryService>();


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
                    builder.WithOrigins("https://34.141.119.232:7136", "http://34.141.119.232:5500", "http://127.0.0.1:5500", "http://localhost:5173","http://job.gjirafalifeproject.life","http://34.159.188.181:8080")
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();

                });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddAdvancedDependencyInjection();

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateEducationCommandValidator>();




            builder.Services.Scan(p => p.FromAssemblies(assemblies)
                .AddClasses()
                .AsMatchingInterface());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
           // if (app.Environment.IsDevelopment())
           // {
                app.UseSwagger();
                app.UseSwaggerUI();
           // }


            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}


            app.MapHub<RealTimeHub>("/realtimehub");

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
