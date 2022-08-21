using System.ComponentModel.DataAnnotations;

namespace Shopping.Application.Resources.Auth.Login;

public class LoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}