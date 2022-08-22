using Microsoft.AspNetCore.Mvc;
using Shopping.API.Controllers.Base;
using Shopping.Application.Resources.Auth.Login;
using Shopping.Application.Resources.Auth.Register;
using Shopping.Application.Services.Abstract;

namespace Shopping.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController : ApplicationControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Login
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<LoginResponse> Login(LoginRequest request)
    {
        return await _authService.Authenticate(request);
    }

    /// <summary>
    /// Register
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<RegisterResponse> Register(RegisterRequest request)
    {
        return await _authService.Register(request);
    }
}