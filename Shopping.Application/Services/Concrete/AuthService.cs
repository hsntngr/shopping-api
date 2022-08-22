using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shopping.Application.Http.Exceptions;
using Shopping.Application.Resources.Auth.Login;
using Shopping.Application.Resources.Auth.Register;
using Shopping.Application.Services.Abstract;
using Shopping.Domain.Entities;
using Shopping.EntityFrameworkCore.Repositories.Abstract;
using Shopping.EntityFrameworkCore.UnitOfWork.Abstract;

namespace Shopping.Application.Services.Concrete;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<LoginResponse> Authenticate(LoginRequest request)
    {
        User user = await _userRepository.FindUserByEmail(request.Email);

        if (user == null)
            throw new BusinessException("Email or password is not valid");

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new BusinessException("Email or password is not valid");

        int expiresInDays = _configuration.GetValue<int>("JWT:ExpiresInDays");

        return new LoginResponse()
        {
            ExpiresIn = expiresInDays * 24 * 60 * 60,
            Token = GenerateTokenForUser(user, expiresInDays)
        };
    }

    public async Task<RegisterResponse> Register(RegisterRequest request)
    {
        User existingUser = await _userRepository.FindUserByEmail(request.Email);

        if (existingUser != null) throw new BusinessException("User with the given email address already exists");

        User user = _mapper.Map<RegisterRequest, User>(request);

        _userRepository.Add(user);

        await _unitOfWork.SaveChangesAsync();

        return new RegisterResponse() {Message = "User registered successfully"};
    }

    private string GenerateTokenForUser(User user, int expiresInDays)
    {
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var keys = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWT:Secret"));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _configuration.GetValue<string>("JWT:Issuer"),
            Subject = new ClaimsIdentity(new[] {new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())}),
            Expires = DateTime.UtcNow.AddDays(expiresInDays),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keys), SecurityAlgorithms.HmacSha256Signature)
        };
        var securityToken = jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
        return jwtSecurityTokenHandler.WriteToken(securityToken);
    }
}