namespace Shopping.Domain.Shared.Validations;

public class UserEntityValidations
{
    public class FirstName
    {
        public const int MinLength = 2;
        public const int MaxLength = 50;
        public const bool IsRequired = true;
    }

    public class LastName
    {
        public const int MinLength = 2;
        public const int MaxLength = 50;
        public const bool IsRequired = true;
    }

    public class Email
    {
        public const int MinLength = 3;
        public const int MaxLength = 80;
        public const bool IsRequired = true;
    }

    public class PasswordHash
    {
        public const int MinLength = 72;
        public const int MaxLength = 72;
        public const bool IsRequired = true;
    }
}