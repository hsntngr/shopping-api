namespace Shopping.Domain.Shared.Validations;

public class ProductEntityValidation
{
    public class Name
    {
        public const bool IsRequired = true;
        public const int MaxLength = 200;
    }
    
    public class Price
    {
        public const bool IsRequired = true;
        public const int Precision = 18;
        public const int Scale = 4;
    }
}