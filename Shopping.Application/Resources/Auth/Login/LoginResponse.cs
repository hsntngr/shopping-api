using System.Diagnostics.CodeAnalysis;

namespace Shopping.Application.Resources.Auth.Login;

public class LoginResponse
{
    public string Token { get; set; }
    public int ExpiresIn { get; set; }
}