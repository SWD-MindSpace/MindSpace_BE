using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MindSpace.Application.Commons.Utilities;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Infrastructure.Persistence;
using MindSpace.Infrastructure.Seeders;

namespace MindSpace.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            try
            {
                var connectionString = configuration.GetConnectionString("MindSpaceDb");
                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString).EnableSensitiveDataLogging());

                // Add Identity services (authentication with tokens and cookies) with role supports, using ApplicationDbContext as the data store for Identity
                services.AddIdentityApiEndpoints<ApplicationUser>()
                    .AddRoles<ApplicationRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

                // Seeders
                services.AddScoped<IFileReader, FileReader>();
                services.AddScoped<IDataSeeder, IdentitySeeder>();
                services.AddScoped<ApplicationDbContextSeeder>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
