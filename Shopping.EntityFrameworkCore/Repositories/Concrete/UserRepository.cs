using Microsoft.EntityFrameworkCore;
using Shopping.Domain.Entities;
using Shopping.EntityFrameworkCore.Contexts;
using Shopping.EntityFrameworkCore.Repositories.Abstract;
using Shopping.EntityFrameworkCore.Repositories.Base.Concrete;

namespace Shopping.EntityFrameworkCore.Repositories.Concrete;

public class UserRepository : Repository<User, Guid>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<User> FindUserByEmail(string email)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.Email == email);
    }
}