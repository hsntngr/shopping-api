using Shopping.Domain.Entities;
using Shopping.EntityFrameworkCore.Seeders.Base;

namespace Shopping.EntityFrameworkCore.Seeders;

public class ProductSeeder : ISeeder<Product>
{
    public ICollection<Product> Seed()
    {
        return new List<Product>
        {
            new(Guid.NewGuid(), "Apple Airpod", 1200),
            new(Guid.NewGuid(), "Monster Notebook", 800),
            new(Guid.NewGuid(), "Apple Macbook", 2400),
            new(Guid.NewGuid(), "Logitech Mouse", 80),
            new(Guid.NewGuid(), "Logitech Keyboard", 120),
            new(Guid.NewGuid(), "Sandisk Flash Drive", 40),
        };
    }
}