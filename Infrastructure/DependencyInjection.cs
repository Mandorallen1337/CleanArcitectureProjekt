using Application.Interfaces.RepoInterface;
using Infrastructure.Databases;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces.BlobStorageInterface;
using Infrastructure.Services.BlobStorageService;
using Microsoft.Extensions.Configuration;
using Application.Interfaces.OpenAiInterface;
using Infrastructure.Services;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Add the DbContext
            services.AddDbContext<Database>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
            // Add other services
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton<IBlobStorage, BlobStorageService>();
            
            return services;
        }
    }
}
