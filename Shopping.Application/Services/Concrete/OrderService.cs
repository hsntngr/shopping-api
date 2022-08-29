using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Shopping.Application.Helperrs;
using Shopping.Application.Http.Exceptions;
using Shopping.Application.Http.Exceptions.Order;
using Shopping.Application.Resources.Order;
using Shopping.Application.Services.Abstract;
using Shopping.Application.Validations.Order;
using Shopping.Domain.Entities;
using Shopping.Domain.Shared.Enums;
using Shopping.EntityFrameworkCore.Repositories.Abstract;
using Shopping.EntityFrameworkCore.UnitOfWork.Abstract;

namespace Shopping.Application.Services.Concrete;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _cartRepository = cartRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ICollection<OrderResponse>> GetList(Guid userId)
    {
        ICollection<Order> orders = await _orderRepository.GetUserOrdersWithDetailsAsync(userId);
        return _mapper.Map<ICollection<Order>, ICollection<OrderResponse>>(orders);
    }

    public async Task<OrderResponse> CreateOrder(CreateOrderRequest createOrderRequest, Guid userId)
    {
        int cartItemsCount = await _cartRepository.CountCartItemsByUserId(userId, createOrderRequest.ProductIds);
        if (cartItemsCount > OrderValidations.OrderItems.MaxCount) throw new OrderItemMaxLimitExceededException();
        if (cartItemsCount < OrderValidations.OrderItems.MinCount) throw new MinimumOrderItemsNotSatisfiedException();

        int cartItemsTotalQuantity = await _cartRepository.SumTotalCartItemsQuantityByUserId(userId, createOrderRequest.ProductIds);
        if (cartItemsTotalQuantity > OrderValidations.OrderItems.TotalQuantity) throw new OrderItemsTotalQuantityLimitExceededException();

        int orderCountOfToday = await _orderRepository.GetOrderCountOfTodayAsync(userId);
        if (orderCountOfToday > OrderValidations.MaxOrderCountEachDay) throw new OrderLimitExceededException();

        ICollection<Cart> cartItems = await _cartRepository.GetCartItemsByUserId(userId, createOrderRequest.ProductIds);

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            ICollection<OrderItem> orderItems = _mapper.Map<ICollection<Cart>, ICollection<OrderItem>>(cartItems);

            Order order = new Order
            {
                UserId = userId,
                Status = OrderStatus.Draft,
                Code = await GenerateUniqueOrderCodeAsync(),
                TotalPrice = cartItems.Sum(x => x.Quantity * x.Product.Price),
                OrderItems = orderItems
            };

            _cartRepository.RemoveRange(cartItems);
            _orderRepository.Add(order);

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            return _mapper.Map<Order, OrderResponse>(order);
        }
        catch (Exception exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw new BusinessException("Unknown occurred while creating order", exception);
        }
    }

    public async Task<OrderResponse> CompleteOrder(Guid orderId, Guid userId)
    {
        Order? order = await _orderRepository.GetOrderWithOrderItemsAsync(orderId);
        if (order == null) throw new NotFoundException();

        int orderItemsCount = order.OrderItems.Count;
        if (order.UserId != userId) throw new UnauthorizedException();
        if (order.Status == OrderStatus.Submitted) throw new OrderAlreadyCompletedException();
        if (orderItemsCount < OrderValidations.OrderItems.MinCount) throw new MinimumOrderItemsNotSatisfiedException();
        if (orderItemsCount > OrderValidations.OrderItems.MaxCount) throw new OrderItemMaxLimitExceededException();

        int orderItemsTotalQuantity = order.OrderItems.Sum(x => x.Quantity);
        if (orderItemsTotalQuantity > OrderValidations.OrderItems.TotalQuantity) throw new OrderItemsTotalQuantityLimitExceededException();

        order.Status = OrderStatus.Submitted;
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<Order, OrderResponse>(order);
    }

    public async Task<string> GenerateUniqueOrderCodeAsync()
    {
        while (true)
        {
            var uniqueKey = UniqueKeyGenerator.Create(10);
            var code = $"PO{uniqueKey}";
            var exist = await _orderRepository.CheckOrderExistsAsync(code);
            if (!exist) return code;
        }
    }
}