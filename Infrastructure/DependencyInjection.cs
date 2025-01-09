using Application.Interfaces.RepoInterface;
using Infrastructure.Databases;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces.BlobStorageInterface;
using Infrastructure.Services.BlobStorageService;
using Microsoft.Extensions.Configuration;


namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<Database>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));        
            //services.AddSingleton<IBlobStorage, BlobStorageService>();

            return services;
        }
    }
}
