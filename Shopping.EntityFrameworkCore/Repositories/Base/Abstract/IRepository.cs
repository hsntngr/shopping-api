using System.Linq.Expressions;
using Shopping.Domain.Base.Abstract;

namespace Shopping.EntityFrameworkCore.Repositories.Base.Abstract;

public interface IRepositoryRepositoryBase<TEntity> where TEntity : class
{
    Task<ICollection<TEntity>> GetAllAsync();
    Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression);
    void Add(TEntity entity);
    void AddRange(ICollection<TEntity> entities);
    void Remove(TEntity entity);
    void RemoveRange(ICollection<TEntity> entities);
}

public interface IRepository<TEntity, TKeyType> : IRepositoryRepositoryBase<TEntity> where TEntity : class, IEntity<TKeyType>
{
    Task<TEntity> GetByIdAsync(TKeyType id);
}

public interface IRepository<TEntity> : IRepositoryRepositoryBase<TEntity> where TEntity : class, IEntity
{
}