using Shopping.Domain.Base.Abstract;

namespace Shopping.EntityFrameworkCore.Seeders.Base;

public interface ISeeder<TEntity> where TEntity : class
{
     ICollection<TEntity> Seed();
}