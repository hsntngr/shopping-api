using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.Entities;
using Shopping.Domain.Shared.Validations;
using Shopping.EntityFrameworkCore.Extensions;

namespace Shopping.EntityFrameworkCore.Configuration;

public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        #region Property Configuration

        builder.ConfigureEntityDefaults();

        builder.Property(x => x.UserId)
            .IsRequired(OrderEntityValidations.UserId.IsRequired);
        builder.Property(x => x.Code)
            .IsRequired(OrderEntityValidations.Code.IsRequired)
            .HasColumnType($"char({OrderEntityValidations.Code.CharLength})");
        builder.Property(x => x.TotalPrice)
            .IsRequired(OrderEntityValidations.TotalPrice.IsRequired)
            .HasPrecision(OrderEntityValidations.TotalPrice.Precision, OrderEntityValidations.TotalPrice.Scale);
        builder.Property(x => x.Status)
            .HasDefaultValue(OrderEntityValidations.OrderStatus.DefaultValue);

        builder.HasIndex(x => x.Code).IsUnique(OrderEntityValidations.Code.IsUnique);

        #endregion

        #region Relation Configuration

        builder.HasOne(x => x.User)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.OrderItems)
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderId);

        #endregion
    }
}