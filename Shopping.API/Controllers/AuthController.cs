using Microsoft.AspNetCore.Mvc;
using Shopping.Application.Abstract;
using Shopping.Application.Resources.Auth;
using Shopping.Application.Resources.Auth.Login;
using Shopping.Application.Resources.Auth.Register;

namespace Shopping.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<LoginResponse> Login(LoginRequest request)
    {
        return await _authService.Authenticate(request);
    }

    [HttpPost]
    public async Task<RegisterResponse> Register(RegisterRequest request)
    {
        return await _authService.Register(request);
    }
}