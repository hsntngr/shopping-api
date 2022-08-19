using Shopping.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shopping.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.Shared.Validations;

namespace Shopping.EntityFrameworkCore.Configuration;

public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        #region Property Configuration
        
        builder.ConfigureEntityDefaults();

        builder.HasKey(x => new { x.ProductId, x.OrderId });

        builder.Property(x => x.Price)
            .IsRequired(OrderItemValidations.Price.IsRequired)
            .HasPrecision(OrderItemValidations.Price.Precision, OrderItemValidations.Price.Scale);


        builder.Property(x => x.Quantity)
            .IsRequired(OrderItemValidations.Quantity.IsRequired);

        builder.HasCheckConstraint("Quantity", $"\"Quantity\" > {OrderItemValidations.Quantity.GreaterThan} AND \"Quantity\" < {OrderItemValidations.Quantity.LessThan}");

        #endregion

        #region Relation Configuration

        builder.HasOne(x => x.Order)
            .WithMany(x => x.OrderItems)
            .HasForeignKey(x => x.OrderId);

        builder.HasOne(x => x.Product)
            .WithMany(x => x.OrderItems)
            .HasForeignKey(x => x.ProductId);

        #endregion
    }
}