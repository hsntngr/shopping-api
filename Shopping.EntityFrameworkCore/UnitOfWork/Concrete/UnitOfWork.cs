using Shopping.EntityFrameworkCore.Contexts;
using Shopping.EntityFrameworkCore.UnitOfWork.Abstract;

namespace Shopping.EntityFrameworkCore.UnitOfWork.Concrete;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}