using AutoMapper;
using Shopping.Application.Http.Exceptions;
using Shopping.Application.Resources.User;
using Shopping.Application.Services.Abstract;
using Shopping.Domain.Entities;
using Shopping.EntityFrameworkCore.Repositories.Abstract;

namespace Shopping.Application.Services.Concrete;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserResponse> GetUserById(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null) throw new NotFoundException();
        
        return _mapper.Map<User, UserResponse>(user);
    }
}