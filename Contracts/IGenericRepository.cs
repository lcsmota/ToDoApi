using ToDoApi.Models;

namespace ToDoApi.Contracts;

public interface IGenericRepository<TEntity, in TCreation, in TUpdate>
    where TEntity : BaseEntity
    where TCreation : class
    where TUpdate : class
{
    Task<TEntity> CreateAsync(TCreation entity);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task UpdateAsync(int id, TUpdate entity);
    Task DeleteAsync(int id);
}
