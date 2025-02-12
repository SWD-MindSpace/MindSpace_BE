﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MindSpace.Application.Specifications;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Services.Authentication;
using MindSpace.Domain.Interfaces.Specifications;

namespace MindSpace.Application.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        // ================================
        // === Fields & Props
        // ================================

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        // ================================
        // === Constructors
        // ================================

        public ApplicationUserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // ================================
        // === Methods
        // ================================

        public async Task<IReadOnlyList<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<IList<ApplicationUser>> GetUsersByRoleAsync(string role)
        {
            return await _userManager.GetUsersInRoleAsync(role);
        }

        public async Task<IReadOnlyList<ApplicationUser>> GetAllUsersWithSpecAsync(ISpecification<ApplicationUser> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<ApplicationUser?> GetUserWithSpec(ISpecification<ApplicationUser> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpecification<ApplicationUser> spec)
        {
            var query = _userManager.Users.AsQueryable();
            return await SpecificationQueryBuilder<ApplicationUser>.BuildCountQuery(query, spec).CountAsync();
        }

        private IQueryable<ApplicationUser> ApplySpecification(ISpecification<ApplicationUser> spec)
        {
            var query = _userManager.Users.AsQueryable();
            return SpecificationQueryBuilder<ApplicationUser>.BuildQuery(query, spec);
        }

        public Task InsertBulkAsync(IEnumerable<(ApplicationUser user, string password)> usersWithPassword)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task InsertAsync(ApplicationUser user, string password)
        {
            // Check for duplicate email
            var existingUserByEmail = await _userManager.FindByEmailAsync(user.Email!);
            if (existingUserByEmail != null)
            {
                throw new DuplicateUserException($"A user with the email {user.Email} already exists.");
            }

            // Check for duplicate username
            var existingUserByUsername = await _userManager.FindByNameAsync(user.UserName!);
            if (existingUserByUsername != null)
            {
                throw new DuplicateUserException($"A user with the username {user.UserName} already exists.");
            }

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new CreateFailedException(user.Email!);
            }
        }

        public async Task AssignRoleAsync(ApplicationUser user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
        }
    }
}