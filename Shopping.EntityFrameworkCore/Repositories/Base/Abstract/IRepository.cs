using System.Linq.Expressions;
using Shopping.Domain.Base.Abstract;

namespace Shopping.EntityFrameworkCore.Repositories.Base.Abstract;

public interface IRepository<TEntity, TKeyType> where TEntity : class, IEntity<TKeyType>
{
    Task<ICollection<TEntity>> GetAll();
    Task<ICollection<TEntity>> Get(Expression<Func<TEntity, bool>> expression);
    void Add(TEntity entity);
    void AddRange(ICollection<TEntity> entities);
    Task<TEntity> GetById(TKeyType id);
    void Remove(TEntity entity);
    void RemoveRange(ICollection<TEntity> entities);
}