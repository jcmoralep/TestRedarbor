namespace Common.Application.Interfaces;
public interface IGenericRepository<TEntity>
    where TEntity : class
{
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> DeleteAsync(int id);
    Task<TEntity> GetByIdAsync(int id);
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity> UpdateAsync(TEntity entity);
}
