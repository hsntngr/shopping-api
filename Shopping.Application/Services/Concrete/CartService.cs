using AutoMapper;
using Shopping.Application.Http.Exceptions;
using Shopping.Application.Resources.Cart;
using Shopping.Application.Services.Abstract;
using Shopping.Domain.Entities;
using Shopping.EntityFrameworkCore.Repositories.Abstract;
using Shopping.EntityFrameworkCore.UnitOfWork.Abstract;

namespace Shopping.Application.Services.Concrete;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CartService(ICartRepository cartRepository, IMapper mapper, IUnitOfWork unitOfWork, IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
    }

    public async Task<ICollection<CartItemResponse>> GetCartItemsByUserId(Guid userId)
    {
        ICollection<Cart> items = await _cartRepository.GetCartItemsByUserId(userId);
        return _mapper.Map<ICollection<Cart>, ICollection<CartItemResponse>>(items);
    }

    public async Task<CartItemResponse> CreateCartItem(CreateCartItemRequest request, Guid userId)
    {
        Cart cart = await _cartRepository.GetCartById(userId, request.ProductId) ?? new Cart()
        {
            ProductId = request.ProductId,
            UserId = userId,
            Quantity = 0
        };

        if (cart.Quantity == 0) _cartRepository.Add(cart);
        cart.Quantity++;

        await _unitOfWork.SaveChangesAsync();

        cart.Product = await _productRepository.GetByIdAsync(cart.ProductId);

        return _mapper.Map<Cart, CartItemResponse>(cart);
    }

    public async Task RemoveCartItem(RemoveCartItemRequest request, Guid userId)
    {
        Cart? cart = await _cartRepository.GetCartById(userId, request.ProductId);

        if (cart == null) throw new NotFoundException();
        if (cart.Quantity == 1) _cartRepository.Remove(cart);
        else cart.Quantity--;

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task RemoveAllCartItems(Guid userId)
    {
        var items = await _cartRepository.GetCartItemsByUserId(userId);
        _cartRepository.RemoveRange(items);
        await _unitOfWork.SaveChangesAsync();
    }
}