using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.Entities;
using Shopping.Domain.Shared.Validations;
using Shopping.EntityFrameworkCore.Extensions;

namespace Shopping.EntityFrameworkCore.Configuration;

public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        #region Property Configuration

        builder.ConfigureEntityDefaults();

        builder.Property(x => x.Name)
            .HasMaxLength(ProductEntityValidation.Name.MaxLength)
            .IsRequired(ProductEntityValidation.Name.IsRequired);

        builder.Property(x => x.Price)
            .IsRequired(ProductEntityValidation.Price.IsRequired)
            .HasPrecision(ProductEntityValidation.Price.Precision, ProductEntityValidation.Price.Scale);
        
        builder.HasQueryFilter(x => x.DeletedAt == null);
        #endregion

        #region Relation Configuration

        builder.HasMany(x => x.OrderItems)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);
        
        #endregion
    }
}