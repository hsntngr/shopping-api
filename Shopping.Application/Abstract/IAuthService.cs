using Shopping.Application.Resources.Auth;
using Shopping.Application.Resources.Auth.Login;
using Shopping.Application.Resources.Auth.Register;

namespace Shopping.Application.Abstract;

public interface IAuthService
{
    Task<LoginResponse> Authenticate(LoginRequest request);
    Task<RegisterResponse> Register(RegisterRequest request);
}