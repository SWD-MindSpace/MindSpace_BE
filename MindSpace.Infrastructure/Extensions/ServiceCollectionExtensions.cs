using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MindSpace.Domain.Entities;
using MindSpace.Infrastructure.Persistence;
using MindSpace.Infrastructure.Seeders;
using Restaurants.Application.Commons.Interfaces.Utilities;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Seeders;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            try
            {
                var connectionString = configuration.GetConnectionString("MindSpaceDb");
                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString).EnableSensitiveDataLogging());

                //Add Identity services (authentication with tokens and cookies) with role supports, using ApplicationDbContext as the data store for Identity
                services.AddIdentityApiEndpoints<ApplicationUser>()
                    .AddRoles<ApplicationRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

                // Add FileReader service
                services.AddScoped<IFileReader, FileReader>();

                // Set Services for Data Seeders
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
