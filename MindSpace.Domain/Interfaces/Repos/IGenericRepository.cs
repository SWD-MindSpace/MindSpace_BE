namespace MindSpace.Domain.Interfaces.Repos;

using Entities;

public interface IGenericRepository<T> where T : BaseEntity
{
    // public Task<IEnumerable<T>> GetAllAsync();
    // public Task<T> GetAsync();

    public int? Insert(T entity);
    public int? Update(T entityToUpdate);
    public int? Delete(T entityToDelete);
    public int? Delete(object id);
}
