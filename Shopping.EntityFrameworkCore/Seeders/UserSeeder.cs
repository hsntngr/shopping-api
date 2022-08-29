using Shopping.Domain.Entities;
using Shopping.EntityFrameworkCore.Seeders.Base;

namespace Shopping.EntityFrameworkCore.Seeders;

public class UserSeeder : ISeeder<User>
{
    public ICollection<User> Seed()
    {
        return new List<User>
        {
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Teoman",
                LastName = "Tıngır",
                Email = "user@mail.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
            }
        };
    }
}