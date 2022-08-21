using System.ComponentModel.DataAnnotations;
using Shopping.Application.Validations.Auth;
using Shopping.Domain.Shared.Validations;

namespace Shopping.Application.Resources.Auth.Register;

public class RegisterRequest
{
    [Required]
    [MinLength(UserEntityValidations.FirstName.MinLength)]
    [MaxLength(UserEntityValidations.FirstName.MaxLength)]
    public string FirstName { get; set; }
    [Required]
    [MinLength(UserEntityValidations.LastName.MinLength)]
    [MaxLength(UserEntityValidations.LastName.MaxLength)]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    [MinLength(UserEntityValidations.Email.MinLength)]
    [MaxLength(UserEntityValidations.Email.MaxLength)]
    public string Email { get; set; }
    [Required]
    [MinLength(RegisterValidations.Password.MinLength)]
    [MaxLength(RegisterValidations.Password.MaxLength)]
    public string Password { get; set; }
}