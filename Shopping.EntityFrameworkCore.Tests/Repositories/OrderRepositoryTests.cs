using FizzWare.NBuilder;
using Shopping.Domain.Entities;
using Shopping.EntityFrameworkCore.Contexts;
using Shopping.EntityFrameworkCore.Repositories.Abstract;
using Shopping.EntityFrameworkCore.Repositories.Concrete;
using Shopping.EntityFrameworkCore.Tests.Contexts;
using Shopping.EntityFrameworkCore.UnitOfWork.Abstract;

namespace Shopping.EntityFrameworkCore.Tests.Repositories;

[TestFixture]
public class OrderRepositoryTests
{
    private IOrderRepository _orderRepository;
    private IUnitOfWork _unitOfWork;
    private AppDbContext _context;

    [SetUp]
    public void Setup()
    {
        var factory = new MockAppDbContextContextFactory();
        _context = factory.Create();
        _orderRepository = new OrderRepository(_context);
        _unitOfWork = new UnitOfWork.Concrete.UnitOfWork(_context);
    }

    [Test]
    public async Task GetAll_InitialOrdersShouldBeEmpty()
    {
        var items = await _orderRepository.GetAllAsync();
        Assert.That(items.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task GetOrderByCodeAsync_ShouldReturnOrder()
    {
        var order = Builder<Order>.CreateNew().Build();
        _orderRepository.Add(order);
        await _unitOfWork.SaveChangesAsync();
        var orderByCode = await _orderRepository.GetOrderByCodeAsync(order.Code);
        Assert.That(orderByCode, Is.EqualTo(order));
    }

    [Test]
    public async Task CheckOrderExistsAsync_ShouldReturnFalseIfOrderNotExist()
    {
        var exist = await _orderRepository.CheckOrderExistsAsync("UniqueCode");
        Assert.That(exist, Is.EqualTo(false));
    }

    [Test]
    public async Task CheckOrderExistsAsync_ShouldReturnTrueIfOrderExist()
    {
        var order = Builder<Order>.CreateNew().Build();
        _orderRepository.Add(order);
        await _unitOfWork.SaveChangesAsync();
        var exist = await _orderRepository.CheckOrderExistsAsync(order.Code);
        Assert.That(exist, Is.EqualTo(true));
    }

    [Test]
    public async Task GetOrderWithOrderItemsAsync_ShouldReturnOrderItems()
    {
        var userId = Guid.NewGuid();
        var orderItems = Builder<OrderItem>.CreateListOfSize(10).Build();
        var order = Builder<Order>.CreateNew()
            .With(x => x.OrderItems = orderItems)
            .With(x => x.UserId = userId)
            .Build();
        _context.Add(order);
        await _unitOfWork.SaveChangesAsync();
        var queriedOrder = await _orderRepository.GetOrderWithOrderItemsAsync(order.Id);
        Assert.That(queriedOrder!.OrderItems, Is.EquivalentTo(orderItems));
    }

    [Test]
    public async Task GetOrderCountOfTodayAsync_ShouldReturnTodaysOrderCount()
    {
        var userId = Guid.NewGuid();
        var orders = Builder<Order>.CreateListOfSize(10)
            .All()
            .With(x => x.UserId = userId)
            .Build();
        var updatedOrder = orders!.FirstOrDefault();

        _orderRepository.AddRange(orders);
        await _unitOfWork.SaveChangesAsync();

        updatedOrder = await _orderRepository.GetByIdAsync(updatedOrder!.Id);
        updatedOrder.CreatedAt = DateTime.Now.AddDays(-10);
        await _unitOfWork.SaveChangesAsync();

        var orderCountOfToday = await _orderRepository.GetOrderCountOfTodayAsync(userId);
        Assert.That(orderCountOfToday, Is.EqualTo(9));
    }

    [Test]
    public async Task GetUserOrdersWithDetailsAsync_ShouldReturnWithOrderItemsAndProducts()
    {
        var userId = Guid.NewGuid();
        var orders = Builder<Order>.CreateListOfSize(10)
            .All()
            .With(x => x.UserId = userId)
            .With(x => x.OrderItems = Builder<OrderItem>.CreateListOfSize(10)
                .All()
                .With(i => i.Product = Builder<Product>.CreateNew().With(x => x.Id = Guid.NewGuid()).Build())
                .Build())
            .Build();

        _orderRepository.AddRange(orders);
        await _unitOfWork.SaveChangesAsync();

        var queriedOrders = await _orderRepository.GetUserOrdersWithDetailsAsync(userId);
        Assert.That(orders, Is.EquivalentTo(queriedOrders));
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}