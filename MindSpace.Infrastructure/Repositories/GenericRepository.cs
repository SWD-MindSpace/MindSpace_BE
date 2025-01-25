namespace MindSpace.Infrastructure.Repositories;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MindSpace.Domain.Interfaces.Repos;
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
    // === Methods
    // ===========================

    /// <summary>
    /// Insert a new record of a entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public int? Insert(T entity)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException($"[{nameof(T)}] Insert {entity} failed.");
            _dbContext.Set<T>().Add(entity);

            return entity.Id;
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex.Message, ex);
        }
        return null;
    }

    /// <summary>
    /// Update Entity
    /// </summary>
    /// <param name="entityToUpdate"></param>
    /// <returns></returns>
    public int? Update(T entityToUpdate)
    {
        try
        {
            if (entityToUpdate == null) throw new ArgumentNullException($"[{nameof(T)}] Update {entityToUpdate} failed.");

            _dbContext.Set<T>().Update(entityToUpdate);
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
            return entityToUpdate.Id;
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex.Message, ex);
        }
        return null;
    }

    /// <summary>
    /// Delete an entity
    /// </summary>
    /// <param name="entityToDelete"></param>
    /// <returns></returns>
    public int? Delete(T entityToDelete)
    {
        try
        {
            if (entityToDelete == null) throw new ArgumentNullException($"[{nameof(T)}] Delete {entityToDelete} not exists.");

            if (_dbContext.Set<T>().Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbContext.Set<T>().Attach(entityToDelete);
            }
            _dbContext.Set<T>().Remove(entityToDelete);
            return entityToDelete.Id;
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex.Message, ex);
        }

        return null;
    }

    /// <summary>
    /// Delete an object base on id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public int? Delete(object id)
    {
        try
        {
            if (id == null) throw new ArgumentNullException($"[{nameof(T)}] Delete {id} failed.");

            T? entityToDelete = _dbContext.Set<T>().Find(id);
            return Delete(entityToDelete);
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex.Message, ex);
        }

        return null;
    }
}
