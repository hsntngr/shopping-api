using AutoMapper;
using Shopping.Application.Http.Exceptions;
using Shopping.Application.Http.Exceptions.Order;
using Shopping.Application.Resources.Order.OrderItem;
using Shopping.Application.Services.Abstract;
using Shopping.Domain.Entities;
using Shopping.Domain.Shared.Enums;
using Shopping.EntityFrameworkCore.Repositories.Abstract;
using Shopping.EntityFrameworkCore.UnitOfWork.Abstract;

namespace Shopping.Application.Services.Concrete;

public class OrderItemService : IOrderItemService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderItemService(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IMapper mapper, IOrderItemRepository orderItemRepository)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _orderItemRepository = orderItemRepository;
    }

    public async Task<ICollection<OrderItemResponse>> GetOrderItems(Guid orderId, Guid userId)
    {
        Order? order = await _orderRepository.GetOrderWithOrderItems(orderId);
        if (order == null) throw new NotFoundException();
        if (order.UserId != userId) throw new UnauthorizedException();

        return _mapper.Map<ICollection<OrderItem>, ICollection<OrderItemResponse>>(order.OrderItems);
    }

    public async Task<OrderItemResponse> IncreaseQuantity(Guid orderId, Guid productId, Guid userId)
    {
        var (order, orderItem) = await ValidateOrder(orderId, productId, userId);
        orderItem.Quantity++;
        order.TotalPrice += orderItem.Price;
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<OrderItem, OrderItemResponse>(orderItem);
    }

    public async Task<OrderItemResponse> DecreaseQuantity(Guid orderId, Guid productId, Guid userId)
    {
        var (order, orderItem) = await ValidateOrder(orderId, productId, userId);
        if (orderItem.Quantity == 1) throw new OrderItemMinQuantityException();
        orderItem.Quantity--;
        order.TotalPrice -= orderItem.Price;
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<OrderItem, OrderItemResponse>(orderItem);
    }

    public async Task Remove(Guid orderId, Guid productId, Guid userId)
    {
        var (order, orderItem) = await ValidateOrder(orderId, productId, userId);
        order.TotalPrice -= orderItem.Price * orderItem.Quantity;
        _orderItemRepository.Remove(orderItem);
        await _unitOfWork.SaveChangesAsync();
    }

    private async Task<Tuple<Order, OrderItem>> ValidateOrder(Guid orderId, Guid productId, Guid userId)
    {
        Order? order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null) throw new NotFoundException();
        if (order.UserId != userId) throw new UnauthorizedException();
        if (order.Status == OrderStatus.Submitted) throw new SubmittedOrderCanNotEditException();

        OrderItem? orderItem = await _orderRepository.GetOrderItemById(productId, userId);
        if (orderItem == null) throw new NotFoundException();

        return Tuple.Create(order, orderItem);
    }
}