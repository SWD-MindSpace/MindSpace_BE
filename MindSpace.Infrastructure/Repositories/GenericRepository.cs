﻿namespace MindSpace.Infrastructure.Repositories;

using Application.Specifications;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Interfaces.Repos;
using Domain.Interfaces.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    // ===========================
    // === Fields & Props
    // ===========================

    private readonly ApplicationDbContext _dbContext;

    private readonly ILogger _logger;

    // ===========================
    // === Constructors
    // ===========================

    public GenericRepository(ApplicationDbContext context, ILogger<GenericRepository<T>> logger)
    {
        _dbContext = context;
        _logger = logger;
    }

    // ===========================
    // === INSERT, UPDATE, DELETE
    // ===========================

    /// <summary>
    ///     Insert a new record of a entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public T Insert(T entity)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException($"[{nameof(T)}] Insert {entity} failed.");
            var addedEntity = _dbContext.Set<T>().Add(entity).Entity;

            return addedEntity;
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex.Message, ex);
        }
        return null;
    }

    /// <summary>
    ///     Update Entity
    /// </summary>
    /// <param name="entityToUpdate"></param>
    /// <returns></returns>
    public T Update(T entityToUpdate)
    {
        try
        {
            if (entityToUpdate == null) throw new ArgumentNullException($"[{nameof(T)}] Update {entityToUpdate} failed.");

            var updatedEntity = _dbContext.Set<T>().Update(entityToUpdate).Entity;
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
            return updatedEntity;
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex.Message, ex);
        }
        return null;
    }

    /// <summary>
    ///     Delete an entity
    /// </summary>
    /// <param name="entityToDelete"></param>
    /// <returns></returns>
    public T Delete(T entityToDelete)
    {
        try
        {
            if (entityToDelete == null) throw new ArgumentNullException($"[{nameof(T)}] Delete {entityToDelete} not exists.");

            if (_dbContext.Set<T>().Entry(entityToDelete).State == EntityState.Detached) _dbContext.Set<T>().Attach(entityToDelete);
            var deletedEntity = _dbContext.Set<T>().Remove(entityToDelete).Entity;
            return deletedEntity;
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex.Message, ex);
        }

        return null;
    }

    /// <summary>
    ///     Delete an object base on id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public T Delete(object id)
    {
        try
        {
            if (id == null) throw new ArgumentNullException($"[{nameof(T)}] Delete {id} failed.");

            var entityToDelete = _dbContext.Set<T>().Find(id);
            return Delete(entityToDelete);
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex.Message, ex);
        }

        return null;
    }

    // ===================================
    // === GET queries with Specification
    // ===================================

    public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        => await ApplySpecification(spec).ToListAsync();

    public async Task<T?> GetBySpecAsync(ISpecification<T> spec)
        => await ApplySpecification(spec).FirstOrDefaultAsync();

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        var query = _dbContext.Set<T>().AsQueryable();
        return await SpecificationQueryBuilder<T>.BuildCountQuery(query, spec).CountAsync();
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        IQueryable<T> query = _dbContext.Set<T>().AsQueryable();
        return SpecificationQueryBuilder<T>.BuildQuery(query, spec);
    }

    // ===========================================
    // === GET queries Projection with AutoMapper
    // ===========================================
    public async Task<TDto?> GetBySpecProjectedAsync<TDto>(ISpecification<T> spec, IConfigurationProvider mapperConfig)
    {
        return await ApplySpecification(spec)
            .ProjectTo<TDto>(mapperConfig)
            .FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<TDto>> GetAllWithSpecProjectedAsync<TDto>(ISpecification<T> spec, IConfigurationProvider mapperConfig)
    {
        return await ApplySpecification(spec)
            .ProjectTo<TDto>(mapperConfig)
            .ToListAsync();
    }
}
