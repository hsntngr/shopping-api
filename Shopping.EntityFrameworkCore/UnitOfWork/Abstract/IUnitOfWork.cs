namespace Shopping.EntityFrameworkCore.UnitOfWork.Abstract;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}