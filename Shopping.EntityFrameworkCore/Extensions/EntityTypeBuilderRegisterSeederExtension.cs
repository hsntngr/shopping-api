using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.EntityFrameworkCore.Seeders.Base;

namespace Shopping.EntityFrameworkCore.Extensions;

public static class EntityTypeBuilderRegisterSeederExtension
{
    public static void RegisterSeeder<TEntity>(this EntityTypeBuilder<TEntity> builder, ISeeder<TEntity> seeder) where TEntity : class
    {
        builder.HasData(seeder.Seed());
    }

}