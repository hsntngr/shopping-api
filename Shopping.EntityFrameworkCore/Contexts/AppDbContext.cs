using Microsoft.EntityFrameworkCore;

namespace Shopping.EntityFrameworkCore.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}