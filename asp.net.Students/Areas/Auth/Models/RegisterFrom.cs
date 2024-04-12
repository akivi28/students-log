using System.ComponentModel.DataAnnotations;

namespace asp.net.Students.Areas.Auth.Models;

public class RegisterForm
{
    [Display(Name = "Login")]
    [Required(ErrorMessage = "Enter the login")]
    [EmailAddress(ErrorMessage = "Login must be email")]
    public string Login { get; set; }
    
    [Required(ErrorMessage = "Enter the password")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Enter the confirm password")]
    public string ConfirmPassword { get; set; }
    
}