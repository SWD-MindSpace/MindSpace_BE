﻿namespace MindSpace.Infrastructure.Extensions;

using Application.Commons.Utilities;
using Application.Commons.Utilities.Seeding;
using Domain.Entities.Identity;
using Domain.Interfaces.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MindSpace.Infrastructure.Persistence;
using Repositories;
using Seeders;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add SqlServer and ConnectionString
        var connectionString = configuration.GetConnectionString("MindSpaceDb");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString).EnableSensitiveDataLogging());

        // Add Identity services (authentication with tokens and cookies) with role supports, using ApplicationDbContext as the data store for Identity
        services.AddIdentityApiEndpoints<ApplicationUser>()
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        // Add Unit Of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Add Seeders
        services.AddScoped<IFileReader, FileReader>();
        services.AddScoped<IIdentitySeeder, IdentitySeeder>();
        services.AddScoped<IEntityOrderProvider, EntityOrderProvider>();
        services.AddScoped<IDataCleaner, DatabaseCleaner>();
        services.AddScoped<ApplicationDbContextSeeder>();
    }
}
