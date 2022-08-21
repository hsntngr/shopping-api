using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Update;
using Shopping.Domain.Base.Abstract;
using Shopping.Domain.Entities;

namespace Shopping.EntityFrameworkCore.Extensions;

public static class EntityTypeBuilderConfigureEntityExtension
{
    public static void ConfigureEntityDefaults<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class
    {
        if (typeof(IEntity<Guid>).IsAssignableFrom(typeof(TEntity))) ConfigureBaseEntity(builder);
        if (typeof(IEditableEntity).IsAssignableFrom(typeof(TEntity))) ConfigureEditableEntity(builder);
        if (typeof(ICreatableEntity).IsAssignableFrom(typeof(TEntity))) ConfigureCreatableEntity(builder);
        if (typeof(ISoftDeleteEntity).IsAssignableFrom(typeof(TEntity))) ConfigureSoftDeleteEntity(builder);
    }

    private static void ConfigureBaseEntity<TEntity>(EntityTypeBuilder<TEntity> builder) where TEntity : class
    {
        builder.HasKey(x => ((IEntity<Guid>) x).Id);
    }

    private static void ConfigureCreatableEntity<TEntity>(EntityTypeBuilder<TEntity> builder) where TEntity : class
    {
        builder.Property<DateTime>(nameof(ICreatableEntity.CreatedAt)).HasDefaultValueSql("NOW()");
        builder.Property(x => ((ICreatableEntity) x).CreatedBy).IsRequired(false);
        builder.HasOne(x => ((ICreatableEntity) x).Creator)
            .WithMany()
            .HasForeignKey(x => ((ICreatableEntity) x).CreatedBy);
    }

    private static void ConfigureEditableEntity<TEntity>(EntityTypeBuilder<TEntity> builder) where TEntity : class
    {
        builder.Property(x => ((IEditableEntity) x).UpdatedBy).IsRequired(false);
        builder.HasOne(x => ((IEditableEntity) x).Modifier)
            .WithMany()
            .HasForeignKey(x => ((IEditableEntity) x).UpdatedBy);
    }

    private static void ConfigureSoftDeleteEntity<TEntity>(EntityTypeBuilder<TEntity> builder) where TEntity : class
    {
        builder.Property(x => ((ISoftDeleteEntity) x).DeletedBy).IsRequired(false);
        builder.HasOne(x => ((ISoftDeleteEntity) x).Deleter)
            .WithMany()
            .HasForeignKey(x => ((ISoftDeleteEntity) x).DeletedBy);
    }
}