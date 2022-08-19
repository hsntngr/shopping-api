namespace Shopping.Domain.Shared.Validations;

public class CartEntityValidations
{
    public class UserId
    {
        public const bool IsRequired = true;
    }

    public class ProductId
    {
        public const bool IsRequired = true;
    }

    public class Quantity
    {
        public const bool IsRequired = true;
        public const int GreaterThan = 0;
        public const int LessThan = 10000;
    }
}