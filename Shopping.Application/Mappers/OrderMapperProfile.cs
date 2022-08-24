using AutoMapper;
using Shopping.Application.Resources.Order;
using Shopping.Domain.Entities;
using Shopping.Domain.Shared.Enums;

namespace Shopping.Application.Mappers;

public class OrderMapperProfile : Profile
{
    public OrderMapperProfile()
    {
        CreateMap<Order, OrderResponse>();
        CreateMap<Cart, OrderItem>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));
    }
}