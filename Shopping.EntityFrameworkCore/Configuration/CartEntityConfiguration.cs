using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.Entities;
using Shopping.Domain.Shared.Validations;
using Shopping.EntityFrameworkCore.Extensions;

namespace Shopping.EntityFrameworkCore.Configuration;

public class CartEntityConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        #region Property Configuration

        builder.ConfigureEntityDefaults();

        builder.HasKey(x => new { x.ProductId, x.UserId });

        builder.Property(x => x.ProductId)
            .IsRequired(CartEntityValidations.ProductId.IsRequired);
        builder.Property(x => x.UserId)
            .IsRequired(CartEntityValidations.UserId.IsRequired);
        builder.Property(x => x.Quantity)
            .IsRequired(OrderItemValidations.Quantity.IsRequired);

        builder.HasCheckConstraint("Quantity", $"\"Quantity\" > {OrderItemValidations.Quantity.GreaterThan} AND \"Quantity\" < {OrderItemValidations.Quantity.LessThan}");

        #endregion

        #region Relational Configuration

        builder.HasOne(x => x.Product)
            .WithMany(x => x.CartItems)
            .HasForeignKey(x => x.ProductId);

        builder.HasOne(x => x.User)
            .WithMany(x => x.CartItems)
            .HasForeignKey(x => x.UserId);

        #endregion
    }
}