using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.API.Controllers.Base;
using Shopping.Application.Resources.Auth.Login;
using Shopping.Application.Resources.Auth.Register;
using Shopping.Application.Resources.User;
using Shopping.Application.Services.Abstract;

namespace Shopping.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController : ApplicationControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public AuthController(IAuthService authService, IUserService userService)
    {
        _authService = authService;
        _userService = userService;
    }

    /// <summary>
    /// Login
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<LoginResponse> Login(LoginRequest request) => await _authService.Authenticate(request);

    /// <summary>
    /// Register
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<RegisterResponse> Register(RegisterRequest request) => await _authService.Register(request);

    /// <summary>
    /// Get Current User Details
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<UserResponse> AuthenticatedUser() => await _userService.GetUserById(CurrentUserId);
}   