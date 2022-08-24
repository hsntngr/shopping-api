using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shopping.Domain.Base.Abstract;
using Shopping.EntityFrameworkCore.Repositories.Base.Abstract;

namespace Shopping.EntityFrameworkCore.Repositories.Base.Concrete;

public abstract class RepositoryBase<TEntity> : IRepositoryRepositoryBase<TEntity> where TEntity : class
{
    protected readonly DbSet<TEntity> DbSet;

    public RepositoryBase(DbContext context)
    {
        DbSet = context.Set<TEntity>();
    }

    public async Task<ICollection<TEntity>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public async Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
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

    public void Remove(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public void RemoveRange(ICollection<TEntity> entities)
    {
        DbSet.RemoveRange(entities);
    }
}

public abstract class Repository<TEntity, TKeyType> : RepositoryBase<TEntity>, IRepository<TEntity, TKeyType> where TEntity : class, IEntity<TKeyType>
{
    public Repository(DbContext context) : base(context)
    {
    }

    public async Task<TEntity> GetByIdAsync(TKeyType id)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }
}

public abstract class Repository<TEntity> : RepositoryBase<TEntity>, IRepository<TEntity> where TEntity : class, IEntity
{
    public Repository(DbContext context) : base(context)
    {
    }
}