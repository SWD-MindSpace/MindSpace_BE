﻿namespace MindSpace.Infrastructure.Seeders;

using Domain.Entities.Constants;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Commons.Constants;
using MindSpace.Application.Interfaces.Utilities.Seeding;
using MindSpace.Infrastructure.Seeders.FakeData;
using Persistence;

public class IdentitySeeder(
    ILogger<IdentitySeeder> logger,
    ApplicationDbContext dbContext,
    UserManager<ApplicationUser> userManager) : IIdentitySeeder
{
    public async Task SeedAsync()
    {
        if (await dbContext.Database.CanConnectAsync())
            try
            {
                IEnumerable<ApplicationUser> users = null;
                if (!dbContext.Roles.Any())
                {
                    var roles = IdentityData.GetRoles();
                    dbContext.Roles.AddRange(roles);
                    await dbContext.SaveChangesAsync();
                }
                if (!dbContext.Users.Any())
                {
                    users = IdentityData.GetUsers();
                    foreach (var user in users) await userManager.CreateAsync(user, AppCts.DefaultPassword);
                }
                if (users is not null && !dbContext.UserRoles.Any()) await GetUserRoles(users);
            }
            catch (Exception ex)
            {
                logger.LogError("{ex}", ex.Message);
            }
    }

    private async Task GetUserRoles(IEnumerable<ApplicationUser> users)
    {
        foreach (var user in users)
        {
            //student one -> student
            var strippedUserRole = user.FullName.ToLower().Split(' ')[0];
            switch (strippedUserRole)
            {
                case "student":
                    await userManager.AddToRoleAsync(user, UserRoles.Student);
                    break;
                case "parent":
                    await userManager.AddToRoleAsync(user, UserRoles.Parent);
                    break;

                case "admin":
                    await userManager.AddToRoleAsync(user, UserRoles.Admin);
                    break;

                case "psychologist":
                    await userManager.AddToRoleAsync(user, UserRoles.Psychologist);
                    break;

                case "schoolmanager":
                    await userManager.AddToRoleAsync(user, UserRoles.SchoolManager);
                    break;
            }
        }
    }
}