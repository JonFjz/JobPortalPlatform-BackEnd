using System.Runtime.Loader;
using JobPortal.Application;
using JobPortal.Persistence;


namespace JobPortal.API
{
    public class Program
    {
        public static void Main(string[] args)
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



            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
