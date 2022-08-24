using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Shopping.Application.Resources.Cart;
using Shopping.Domain.Entities;

namespace Shopping.Application.Mappers;

public class CartMapperProfile : Profile
{
    public CartMapperProfile()
    {
        CreateMap<CreateCartItemRequest, Cart>();
        CreateMap<Cart, CartItemResponse>();
    }
}