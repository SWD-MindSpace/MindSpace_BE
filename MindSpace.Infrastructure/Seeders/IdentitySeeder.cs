using Microsoft.AspNetCore.Identity;
using MindSpace.Application.Commons.Utilities;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Infrastructure.Persistence;

namespace MindSpace.Infrastructure.Seeders
{
    internal class IdentitySeeder(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager) : IDataSeeder
    {
        public async Task SeedAsync()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    dbContext.Roles.AddRange(roles);
                    await dbContext.SaveChangesAsync();
                }
                if (!dbContext.Users.Any())
                {
                    var users = GetUsers();
                    foreach (var user in users)
                    {
                        await userManager.CreateAsync(user, "Password1!");
                    }
                }
                if (!dbContext.UserRoles.Any())
                {
                    var userRoles = GetUserRoles();
                    dbContext.UserRoles.AddRange(userRoles);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<ApplicationUser> GetUsers()
        {
            List<ApplicationUser> users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    UserName = "student1",
                    NormalizedUserName = "STUDENT1",
                    Email = "student1@example.com",
                    NormalizedEmail = "STUDENT1@EXAMPLE.COM",
                    FullName = "Student One",
                    Status = UserStatus.Enabled,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ApplicationUser
                {
                    UserName = "admin1",
                    NormalizedUserName = "ADMIN1",
                    Email = "admin1@example.com",
                    NormalizedEmail = "ADMIN1@EXAMPLE.COM",
                    FullName = "Admin One",
                    Status = UserStatus.Enabled,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ApplicationUser
                {
                    UserName = "psychologist1",
                    NormalizedUserName = "PSYCHOLOGIST1",
                    Email = "psychologist1@example.com",
                    NormalizedEmail = "PSYCHOLOGIST1@EXAMPLE.COM",
                    FullName = "Psychologist One",
                    Status = UserStatus.Enabled,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ApplicationUser
                {
                    UserName = "schoolmanager1",
                    NormalizedUserName = "SCHOOLMANAGER1",
                    Email = "schoolmanager1@example.com",
                    NormalizedEmail = "SCHOOLMANAGER1@EXAMPLE.COM",
                    FullName = "School Manager One",
                    Status = UserStatus.Enabled,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ApplicationUser
                {
                    UserName = "parent1",
                    NormalizedUserName = "PARENT1",
                    Email = "parent1@example.com",
                    NormalizedEmail = "PARENT1@EXAMPLE.COM",
                    FullName = "Parent One",
                }
            };

            return users;
        }

        private IEnumerable<ApplicationRole> GetRoles()
        {
            List<ApplicationRole> roles = new List<ApplicationRole>
            {
                new ApplicationRole(UserRoles.Student) {
                    NormalizedName = UserRoles.Student.ToUpper()
                },
                new ApplicationRole(UserRoles.Admin) {
                    NormalizedName = UserRoles.Admin.ToUpper()
                },
                new ApplicationRole(UserRoles.Psychologist) {
                    NormalizedName = UserRoles.Psychologist.ToUpper()
                },
                new ApplicationRole(UserRoles.SchoolManager) {
                    NormalizedName = UserRoles.SchoolManager.ToUpper()
                },
                new ApplicationRole(UserRoles.Parent) {
                    NormalizedName = UserRoles.Parent.ToUpper()
                },
            };

            return roles;
        }

        private IEnumerable<IdentityUserRole<int>> GetUserRoles()
        {
            var users = dbContext.Users.ToDictionary(u => u.UserName, u => u.Id);
            var roles = dbContext.Roles.ToDictionary(r => r.Name, r => r.Id);

            return new List<IdentityUserRole<int>>
            {
                new IdentityUserRole<int> { UserId = users["student1"], RoleId = roles[UserRoles.Student] },
                new IdentityUserRole<int> { UserId = users["admin1"], RoleId = roles[UserRoles.Admin] },
                new IdentityUserRole<int> { UserId = users["psychologist1"], RoleId = roles[UserRoles.Psychologist] },
                new IdentityUserRole<int> { UserId = users["schoolmanager1"], RoleId = roles[UserRoles.SchoolManager] },
                new IdentityUserRole<int> { UserId = users["parent1"], RoleId = roles[UserRoles.Parent] }
            };
        }
    }
}