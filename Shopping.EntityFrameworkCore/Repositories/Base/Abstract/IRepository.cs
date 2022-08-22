using System.Linq.Expressions;
using Shopping.Domain.Base.Abstract;

namespace Shopping.EntityFrameworkCore.Repositories.Base.Abstract;

public interface IRepositoryRepositoryBase<TEntity> where TEntity : class
{
    Task<ICollection<TEntity>> GetAll();
    Task<ICollection<TEntity>> Get(Expression<Func<TEntity, bool>> expression);
    void Add(TEntity entity);
    void AddRange(ICollection<TEntity> entities);
    void Remove(TEntity entity);
    void RemoveRange(ICollection<TEntity> entities);
}

public interface IRepository<TEntity, TKeyType> : IRepositoryRepositoryBase<TEntity> where TEntity : class, IEntity<TKeyType>
{
    Task<TEntity> GetById(TKeyType id);
}

public interface IRepository<TEntity> : IRepositoryRepositoryBase<TEntity> where TEntity : class, IEntity
{
}