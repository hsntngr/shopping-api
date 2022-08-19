namespace Shopping.Domain.Shared.Validations;

public class OrderItemValidations
{
    public class Price
    {
        public const bool IsRequired = true;
        public const int Precision = 18;
        public const int Scale = 4;
    }

    public class Quantity
    {
        public const bool IsRequired = true;
        public const int GreaterThan = 0;
        public const int LessThan = 10000;
    }
}