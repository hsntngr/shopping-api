using AutoMapper;
using Shopping.Application.Resources.User;
using Shopping.Domain.Entities;

namespace Shopping.Application.Mappers;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<User, UserResponse>();
    }
}