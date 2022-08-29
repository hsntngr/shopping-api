using Shopping.Application.Resources.User;

namespace Shopping.Application.Services.Abstract;

public interface IUserService
{
    Task<UserResponse> GetUserById(Guid userId);
}