using AutoMapper;
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

    public OrderService(IOrderRepository orderRepository, IMapper mapper, IUnitOfWork unitOfWork, ICartRepository cartRepository)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _cartRepository = cartRepository;
    }

    public async Task<ICollection<OrderResponse>> GetList(Guid userId)
    {
        ICollection<Order> orders = await _orderRepository.GetAsync(x => x.UserId == userId);
        return _mapper.Map<ICollection<Order>, ICollection<OrderResponse>>(orders);
    }

    public async Task<OrderResponse> Create(Guid userId)
    {
        int cartItemsCount = await _cartRepository.CountTotalCartItemsByUserId(userId);

        if (cartItemsCount < OrderValidations.MinOrderItemsCount) throw new MinimumOrderItemsNotSatisfiedException();
        if (cartItemsCount > OrderValidations.MaxOrderItemsCount) throw new TotalOrderItemsLimitExceededException();

        Cart? cartItemHasMaxQuantity = await _cartRepository.GetCartItemHasMaxQuantity(userId);

        if (cartItemHasMaxQuantity != null && cartItemHasMaxQuantity.Quantity > OrderValidations.OrderItems.MaxCount) throw new OrderItemLimitExceededException();

        int orderCountOfToday = await _orderRepository.GetOrderCountOfToday(userId);
        if (orderCountOfToday > OrderValidations.MaxOrderCountEachDay) throw new OrderLimitExceededException();

        ICollection<Cart> cartItems = await _cartRepository.GetCartItemsByUserId(userId);

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
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw new BusinessException("Unknown occurred while creating order");
        }
    }

    public async Task<string> GenerateUniqueOrderCodeAsync()
    {
        while (true)
        {
            var uniqueKey = UniqueKeyGenerator.Create(10);
            var code = $"PO{uniqueKey}";
            var exist = await _orderRepository.CheckOrderExists(code);
            if (!exist) return code;
        }
    }
}