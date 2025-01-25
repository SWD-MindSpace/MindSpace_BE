namespace MindSpace.Infrastructure.Repositories;

using Domain.Entities;
using Microsoft.Extensions.Logging;
using MindSpace.Domain.Interfaces.Repos;
using MindSpace.Infrastructure.Persistence;
using System.Collections.Concurrent;

public class UnitOfWork : IUnitOfWork
{
    // ===================================
    // === Fields & Prop
    // ===================================

    private readonly ApplicationDbContext _dbContext;
    private readonly ILoggerFactory _loggerFactory;
    private ConcurrentDictionary<string, object> _repos;

    // ===================================
    // === Constructors
    // ===================================
    public UnitOfWork(ApplicationDbContext context, ILoggerFactory loggerFactory)
    {
        _dbContext = context;
        _loggerFactory = loggerFactory;
    }

    // ===================================
    // === Methods
    // ===================================

    /// <summary>
    /// Dispose object
    /// </summary>
    public void Dispose()
    {
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Save change async
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<int> CompleteAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// For example type = "Customer", then 1 GenericRepository of type Customer is created or acccesed if it's created
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public IGenericRepository<T> Repository<T>() where T : BaseEntity
    {
        if (_repos == null)
        {
            _repos = new ConcurrentDictionary<string, object>();
        }

        var typeEntityName = typeof(T).Name;

        // Using reflection to create an instanceof GenericRepository with type T
        // Passing db context for each repository
        var repoInstanceTypeT = _repos.GetOrAdd(typeEntityName, _ =>
        {
            var repoLogger = _loggerFactory.CreateLogger(typeEntityName);
            var repoType = typeof(GenericRepository<T>);

            var repoInstance = Activator.CreateInstance(
                repoType,
                _dbContext,
                repoLogger);

            return repoInstance;
        });

        return (IGenericRepository<T>)repoInstanceTypeT;
    }
}