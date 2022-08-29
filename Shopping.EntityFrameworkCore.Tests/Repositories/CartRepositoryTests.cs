using FizzWare.NBuilder;
using Shopping.Domain.Entities;
using Shopping.EntityFrameworkCore.Contexts;
using Shopping.EntityFrameworkCore.Repositories.Abstract;
using Shopping.EntityFrameworkCore.Repositories.Concrete;
using Shopping.EntityFrameworkCore.Tests.Contexts;
using Shopping.EntityFrameworkCore.UnitOfWork.Abstract;

namespace Shopping.EntityFrameworkCore.Tests.Repositories;

[TestFixture]
public class CartRepositoryTests
{
    private IProductRepository _productRepository;
    private ICartRepository _cartRepository;
    private IUserRepository _userRepository;
    private IUnitOfWork _unitOfWork;
    private AppDbContext _context;

    [SetUp]
    public void Setup()
    {
        var factory = new MockAppDbContextContextFactory();
        _context = factory.Create();
        _productRepository = new ProductRepository(_context);
        _cartRepository = new CartRepository(_context);
        _userRepository = new UserRepository(_context);
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
    public async Task GetAll_InitialCartShouldBeEmpty()
    {
        var items = await _cartRepository.GetAllAsync();
        Assert.That(items.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task GetCartItemsByUserId_ShouldBeEqualToCreatedCartItems()
    {
        var user = Builder<User>.CreateNew().Build();
        user.Id = Guid.NewGuid();
        _userRepository.Add(user);
        await _context.SaveChangesAsync();
        var products = await _productRepository.GetAllAsync();
        var items = products.Select(x => new Cart()
            {
                UserId = user.Id,
                ProductId = x.Id,
                Quantity = 10
            })
            .ToList();
        _cartRepository.AddRange(items);
        await _unitOfWork.SaveChangesAsync();
        Assert.That(await _cartRepository.GetCartItemsByUserId(user.Id), Is.EquivalentTo(items));
    }

    [Test]
    public async Task CountCartItemsByUserId_ShouldBeEqualCreatedCartItemsCount()
    {
        var user = Builder<User>.CreateNew().Build();
        user.Id = Guid.NewGuid();
        _userRepository.Add(user);
        await _context.SaveChangesAsync();
        var products = await _productRepository.GetAllAsync();
        var items = products.Select(x => new Cart()
            {
                UserId = user.Id,
                ProductId = x.Id,
                Quantity = 10
            })
            .ToList();
        _cartRepository.AddRange(items);
        await _unitOfWork.SaveChangesAsync();
        var userCartItemsCount = await _cartRepository.CountCartItemsByUserId(user.Id, products.Select(x => x.Id).ToList());
        Assert.That(products.Count, Is.EqualTo(userCartItemsCount));
    }

    [Test]
    public async Task SumTotalCartItemsQuantityByUserId_ShouldMatchSumOfQuantity()
    {
        var user = Builder<User>.CreateNew().Build();
        user.Id = Guid.NewGuid();
        _userRepository.Add(user);
        await _context.SaveChangesAsync();
        var products = await _productRepository.GetAllAsync();
        var items = products.Select(x => new Cart()
            {
                UserId = user.Id,
                ProductId = x.Id,
                Quantity = 10
            })
            .ToList();
        _cartRepository.AddRange(items);
        await _unitOfWork.SaveChangesAsync();
        var userCartItemQuantitySum = await _cartRepository.SumTotalCartItemsQuantityByUserId(user.Id, products.Select(x => x.Id).ToList());
        Assert.That(userCartItemQuantitySum, Is.EqualTo(items.Sum(x => x.Quantity)));
    }

    [Test]
    public async Task GetCartWithDetailsById_ShouldHaveEagerLoadedProduct()
    {
        var user = Builder<User>.CreateNew().Build();
        user.Id = Guid.NewGuid();
        _userRepository.Add(user);
        await _context.SaveChangesAsync();
        var products = await _productRepository.GetAllAsync();
        var items = products.Select(x => new Cart()
            {
                UserId = user.Id,
                ProductId = x.Id,
                Quantity = 10
            })
            .ToList();
        _cartRepository.AddRange(items);
        await _unitOfWork.SaveChangesAsync();
        var firstProduct = products.FirstOrDefault();
        var cartItem = await _cartRepository.GetCartWithDetailsById(user.Id, firstProduct.Id);
        Assert.That(firstProduct, Is.EqualTo(cartItem?.Product));
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}