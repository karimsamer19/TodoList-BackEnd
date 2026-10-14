using System.ComponentModel.DataAnnotations;

namespace TodoList.DTOs;

public class RegisterDto
{
    [Required]
    public string Username { get; set; }

    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string? Eamil { get; set; }
    [Required]
    public string Password { get; set; }

    [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone Number must be exactly 11 digits")]
    public string? PhoneNumber { get; set; }
}
