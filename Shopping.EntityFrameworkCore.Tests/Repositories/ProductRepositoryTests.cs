using FizzWare.NBuilder;
using Shopping.Domain.Entities;
using Shopping.EntityFrameworkCore.Contexts;
using Shopping.EntityFrameworkCore.Repositories.Abstract;
using Shopping.EntityFrameworkCore.Repositories.Concrete;
using Shopping.EntityFrameworkCore.Tests.Contexts;
using Shopping.EntityFrameworkCore.UnitOfWork.Abstract;

namespace Shopping.EntityFrameworkCore.Tests.Repositories;

[TestFixture]
public class ProductRepositoryTests
{
    private IProductRepository _repository;
    private IUnitOfWork _unitOfWork;
    private AppDbContext _context;

    [SetUp]
    public void Setup()
    {
        var factory = new MockAppDbContextContextFactory();
        _context = factory.Create();
        _repository = new ProductRepository(_context);
        _unitOfWork = new UnitOfWork.Concrete.UnitOfWork(_context);
        _context.AddRange(Builder<Product>
            .CreateListOfSize(20)
            .All()
            .Build()
        );

        _context.AddRange(Builder<User>
            .CreateListOfSize(10)
            .All()
            .Build()
        );
        _context.SaveChanges();
    }

    [Test]
    public async Task GetAll_ShouldEqual20()
    {
        var items = await _repository.GetAllAsync();
        Assert.That(items.Count, Is.EqualTo(20));
    }

    [Test]
    public async Task Add_ShouldAddNewItem()
    {
        Product product = Builder<Product>.CreateNew().Build();
        product.Id = Guid.NewGuid();;
        _repository.Add(product);
        await _unitOfWork.SaveChangesAsync();
        ICollection<Product> items = await _repository.GetAllAsync();
        Assert.That(items.Count, Is.EqualTo(21));
    }

    [Test]
    public async Task Remove_ShouldRemoveProduct()
    {
        ICollection<Product> products = await _repository.GetAllAsync();
        _repository.RemoveRange(products);
        var result = await _unitOfWork.SaveChangesAsync();
        Assert.That(result, Is.EqualTo(20));
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();;
    }
}