﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Commons.Utilities.Seeding;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Infrastructure.Persistence;

namespace MindSpace.Infrastructure.Seeders
{
    public class IdentitySeeder(
        ILogger<IdentitySeeder> logger,
        ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager) : IIdentitySeeder
    {
        private static readonly string Password = "Password1!";
        public async Task SeedAsync()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
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
                        foreach (var user in users)
                        {
                            await userManager.CreateAsync(user, Password);
                        }
                    }
                    if (users is not null && !dbContext.UserRoles.Any()) await GetUserRoles(users);
                }
                catch (Exception ex)
                {
                    logger.LogError("{ex}", ex.Message);
                }
            }
        }

        private async Task GetUserRoles(IEnumerable<ApplicationUser> users)
        {
            foreach (var user in users)
            {
                switch (user.FullName.ToLower())
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

                    case "manager":
                        await userManager.AddToRoleAsync(user, UserRoles.Manager);
                        break;
                }
            }
        }

    }
}