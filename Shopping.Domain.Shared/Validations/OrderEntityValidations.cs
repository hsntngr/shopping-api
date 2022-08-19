namespace Shopping.Domain.Shared.Validations;

public class OrderEntityValidations
{
    public class UserId
    {
        public const bool IsRequired = true;
    }

    public class Code
    {
        public const bool IsRequired = true;
        public const bool IsUnique = true;
        public const int CharLength = 12;
    }

    public class OrderStatus
    {
        public const Enums.OrderStatus DefaultValue = Enums.OrderStatus.Draft;
    }

    public class TotalPrice
    {
        public const bool IsRequired = true;
        public const int Precision = 18;
        public const int Scale = 4;
    }
}