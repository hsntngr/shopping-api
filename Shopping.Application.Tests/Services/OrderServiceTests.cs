using AutoMapper;
using FizzWare.NBuilder;
using Shopping.Application.Http.Exceptions.Order;
using Shopping.Application.Mappers;
using Shopping.Application.Resources.Order;
using Shopping.Application.Services.Abstract;
using Shopping.Application.Services.Concrete;
using Shopping.Application.Validations.Order;
using Shopping.Domain.Entities;
using Shopping.EntityFrameworkCore.Contexts;
using Shopping.EntityFrameworkCore.Repositories.Abstract;
using Shopping.EntityFrameworkCore.Repositories.Concrete;
using Shopping.EntityFrameworkCore.Tests.Contexts;
using Shopping.EntityFrameworkCore.UnitOfWork.Abstract;
using Shopping.EntityFrameworkCore.UnitOfWork.Concrete;

namespace Shopping.Application.Tests.Services;

[TestFixture]
public class OrderServiceTests
{
    private IOrderService _orderService;
    private IOrderRepository _orderRepository;
    private ICartRepository _cartRepository;
    private IUnitOfWork _unitOfWork;
    private User _currentUser;
    private AppDbContext _context;

    [SetUp]
    public void Setup()
    {
        var mapperConfiguration = new MapperConfiguration(configure =>
        {
            configure.AddProfile<OrderMapperProfile>();
            configure.AddProfile<ProductMapperProfile>();
        });
        _context = new MockAppDbContextContextFactory().Create();
        _currentUser = Builder<User>.CreateNew().Build();
        var products = Builder<Product>.CreateListOfSize(OrderValidations.OrderItems.MaxCount).Build();
        var cartItems = products.Select(x => new Cart()
        {
            ProductId = x.Id,
            UserId = _currentUser.Id,
            Quantity = 5
        });
        _context.Add(_currentUser);
        _context.AddRange(products);
        _context.AddRange(cartItems);
        _context.SaveChanges();


        _orderRepository = new OrderRepository(_context);
        _unitOfWork = new UnitOfWork(_context);
        _cartRepository = new CartRepository(_context);
        _orderService = new OrderService(_orderRepository, _cartRepository, new Mapper(mapperConfiguration), _unitOfWork);
    }

    [Test]
    public async Task CreateOrder_ShouldCreateNewOrder()
    {
        var cartItems = await _cartRepository.GetCartItemsByUserId(_currentUser.Id);
        CreateOrderRequest request = new CreateOrderRequest()
        {
            ProductIds = cartItems.Select(x => x.ProductId).ToList()
        };
        var orderResponse = await _orderService.CreateOrder(request, _currentUser.Id);
        Assert.That(orderResponse, Is.TypeOf<OrderResponse>());
    }

    [Test]
    public async Task CreateOrder_ShouldRemoveCartItems()
    {
        var cartItems = await _cartRepository.GetCartItemsByUserId(_currentUser.Id);
        CreateOrderRequest request = new CreateOrderRequest()
        {
            ProductIds = cartItems.Select(x => x.ProductId).ToList()
        };
        await _orderService.CreateOrder(request, _currentUser.Id);
        var queriesCartItems = await _cartRepository.GetCartItemsByUserId(_currentUser.Id);
        Assert.That(queriesCartItems.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task CreateOrder_ShouldThrowMinimumOrderItemsNotSatisfiedExceptionIfNoOrderItem()
    {
        CreateOrderRequest request = new CreateOrderRequest()
        {
            ProductIds = new List<Guid>()
        };
        try
        {
            await _orderService.CreateOrder(request, _currentUser.Id);
        }
        catch (Exception e)
        {
            Assert.That(e, Is.TypeOf<MinimumOrderItemsNotSatisfiedException>());
        }
    }

    [Test]
    public async Task CreateOrder_ShouldThrowOrderItemMaxLimitExceededExceptionIfOrderItemsCountGreaterThanMaxLimit()
    {
        ICollection<Product> products = Builder<Product>.CreateListOfSize(5).All().With(x => x.Id = Guid.NewGuid()).Build();
        ICollection<Cart> cartItems = products.Select(x => new Cart()
        {
            ProductId = x.Id,
            UserId = _currentUser.Id,
            Quantity = 5
        }).ToList();
        _context.AddRange(products);
        _context.AddRange(cartItems);
        await _context.SaveChangesAsync();

        var allCartItems = await _cartRepository.GetCartItemsByUserId(_currentUser.Id);

        CreateOrderRequest request = new CreateOrderRequest()
        {
            ProductIds = allCartItems.Select(x => x.ProductId).ToList()
        };

        try
        {
            await _orderService.CreateOrder(request, _currentUser.Id);
        }
        catch (Exception e)
        {
            Assert.That(e, Is.TypeOf<OrderItemMaxLimitExceededException>());
        }
    }

    [Test]
    public async Task CreateOrder_ShouldThrowOrderItemsTotalQuantityLimitExceededExceptionIfOrderItemsQuantitySumGreaterThanMaxLimit()
    {
        ICollection<Product> products = Builder<Product>.CreateListOfSize(5).All().With(x => x.Id = Guid.NewGuid()).Build();
        var initialCartItems = await _cartRepository.GetCartItemsByUserId(_currentUser.Id);
        ICollection<Cart> cartItems = products.Select(x => new Cart()
        {
            ProductId = x.Id,
            UserId = _currentUser.Id,
            Quantity = OrderValidations.OrderItems.TotalQuantity
        }).ToList();
        _context.RemoveRange(initialCartItems); // bypass OrderItemMaxLimitExceededException
        _context.AddRange(products);
        _context.AddRange(cartItems);
        await _context.SaveChangesAsync();

        var allCartItems = await _cartRepository.GetCartItemsByUserId(_currentUser.Id);

        CreateOrderRequest request = new CreateOrderRequest()
        {
            ProductIds = allCartItems.Select(x => x.ProductId).ToList()
        };

        try
        {
            await _orderService.CreateOrder(request, _currentUser.Id);
        }
        catch (Exception e)
        {
            Assert.That(e, Is.TypeOf<OrderItemsTotalQuantityLimitExceededException>());
        }
    }

    [Test]
    public async Task CreateOrder_ShouldThrowOrderLimitExceededExceptionIfGivenOrderCountADayExceedsMaxLimit()
    {
        var cartItems = await _cartRepository.GetCartItemsByUserId(_currentUser.Id);
        var createOrderRequests = cartItems.Select(x => new CreateOrderRequest() {ProductIds = new List<Guid> {x.ProductId}}).ToList();

        foreach (var createOrderRequest in createOrderRequests)
        {
            await _orderService.CreateOrder(createOrderRequest, _currentUser.Id);
        }

        var product = Builder<Product>.CreateNew().With(x => x.Id = Guid.NewGuid()).Build();
        var cartItem = new Cart {ProductId = product.Id, UserId = _currentUser.Id};
        _context.Add(product);
        _context.Add(cartItem);
        await _context.SaveChangesAsync();

        try
        {
            CreateOrderRequest request = new CreateOrderRequest {ProductIds = new List<Guid> {product.Id}};
            await _orderService.CreateOrder(request, _currentUser.Id);
        }
        catch (Exception e)
        {
            Assert.That(e, Is.TypeOf<OrderLimitExceededException>());
        }
    }
}