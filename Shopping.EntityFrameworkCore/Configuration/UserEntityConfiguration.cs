using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.Entities;
using Shopping.Domain.Shared.Validations;
using Shopping.EntityFrameworkCore.Extensions;

namespace Shopping.EntityFrameworkCore.Configuration;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        #region Configure Properties

        builder.ConfigureEntityDefaults();

        builder.Property(x => x.FirstName)
            .HasMaxLength(UserEntityValidations.FirstName.MaxLength)
            .IsRequired(UserEntityValidations.FirstName.IsRequired);

        builder.Property(x => x.LastName)
            .HasMaxLength(UserEntityValidations.LastName.MaxLength)
            .IsRequired(UserEntityValidations.LastName.IsRequired);

        builder.Property(x => x.Email)
            .HasMaxLength(UserEntityValidations.Email.MaxLength)
            .IsRequired(UserEntityValidations.Email.IsRequired);

        builder.Property(x => x.PasswordHash)
            .HasMaxLength(UserEntityValidations.PasswordHash.MaxLength)
            .IsRequired(UserEntityValidations.PasswordHash.IsRequired);

        #endregion
    }
}