using AutoMapper;
using Shopping.Application.Resources.Auth.Register;
using Shopping.Domain.Entities;

namespace Shopping.Application.Mappers;

public class AuthMapperProfile : Profile
{
    public AuthMapperProfile()
    {
        CreateMap<RegisterRequest, User>()
            .ForMember(x => x.PasswordHash, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)));
    }
}