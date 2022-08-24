using AutoMapper;
using Shopping.Application.Resources.Cart;
using Shopping.Application.Resources.Product;
using Shopping.Domain.Entities;

namespace Shopping.Application.Mappers;

public class ProductMapperProfile : Profile
{
    public ProductMapperProfile()
    {
        CreateMap<CreateProductRequest, Product>();
        CreateMap<UpdateProductRequest, Product>();
        CreateMap<Product, ProductResponse>();
    }
}