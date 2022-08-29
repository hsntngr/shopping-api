using System.Net.NetworkInformation;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shopping.Domain.Base.Abstract;
using Shopping.Domain.Entities;
using Shopping.EntityFrameworkCore.Extensions;
using Shopping.EntityFrameworkCore.Seeders;

namespace Shopping.EntityFrameworkCore.Contexts;

public class AppDbContext : DbContext
{
    private readonly IHttpContextAccessor _context;

    public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor context) : base(options)
    {
        _context = context;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        RegisterSeeders(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override int SaveChanges()
    {
        UpdateEntitiesAuditedProperties();

        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        UpdateEntitiesAuditedProperties();

        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateEntitiesAuditedProperties()
    {
        var entries = ChangeTracker.Entries();
        Guid? userId = _context.HttpContext.Items.ContainsKey("CurrentUserId") ? Guid.Parse((string) _context.HttpContext.Items["CurrentUserId"]) : null;

        foreach (var entry in entries)
        {
            if (typeof(ICreatableEntity).IsAssignableFrom(entry.Entity.GetType()) && entry.State == EntityState.Added)
            {
                ((ICreatableEntity) entry.Entity).CreatedAt = DateTime.UtcNow;
                ((ICreatableEntity) entry.Entity).CreatedBy = userId;
            }

            if (typeof(IEditableEntity).IsAssignableFrom(entry.Entity.GetType()) && entry.State == EntityState.Modified)
            {
                ((IEditableEntity) entry.Entity).UpdatedAt = DateTime.UtcNow;
                ((IEditableEntity) entry.Entity).UpdatedBy = userId;
            }

            if (typeof(ISoftDeleteEntity).IsAssignableFrom(entry.Entity.GetType()) && entry.State == EntityState.Deleted)
            {
                ((ISoftDeleteEntity) entry.Entity).DeletedAt = DateTime.UtcNow;
                ((ISoftDeleteEntity) entry.Entity).DeletedBy = userId;
                entry.State = EntityState.Modified;
            }
        }
    }

    private void RegisterSeeders(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().RegisterSeeder(new ProductSeeder());
        modelBuilder.Entity<User>().RegisterSeeder(new UserSeeder());
    }
}