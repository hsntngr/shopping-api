using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shopping.Domain.Base.Abstract;
using Shopping.EntityFrameworkCore.Contexts;
using Shopping.EntityFrameworkCore.Repositories.Base.Abstract;

namespace Shopping.EntityFrameworkCore.Repositories.Base.Concrete;

public class Repository<TEntity, TKeyType> : IRepository<TEntity, TKeyType> where TEntity : class, IEntity<TKeyType>
{
    protected readonly DbSet<TEntity> DbSet;

    public Repository(DbContext context)
    {
        DbSet = context.Set<TEntity>();
    }   

    public async Task<ICollection<TEntity>> GetAll()
    {
        return await DbSet.ToListAsync();
    }

    public async Task<ICollection<TEntity>> Get(Expression<Func<TEntity, bool>> expression)
    {
        return await DbSet.Where(expression).ToListAsync();
    }

    public void Add(TEntity entity)
    {
        DbSet.Add(entity);
    }

    public void AddRange(ICollection<TEntity> entities)
    {
        DbSet.AddRange(entities);
    }

    public async Task<TEntity> GetById(TKeyType id)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public void Remove(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public void RemoveRange(ICollection<TEntity> entities)
    {
        DbSet.RemoveRange(entities);
    }
}