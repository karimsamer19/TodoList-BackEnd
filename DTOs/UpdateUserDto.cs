
using System.ComponentModel.DataAnnotations;

namespace TodoList.DTOs;

public class UpdateUserDto
{
    [StringLength(50, MinimumLength = 3,
    ErrorMessage = "Username must be between 3 and 50 characters.")]
    public string? Username { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [StringLength(100,
        ErrorMessage = "Email cannot exceed 100 characters.")]
    public string? Email { get; set; }

    [Phone(ErrorMessage = "Invalid phone number.")]
    [StringLength(20,
        ErrorMessage = "Phone number cannot exceed 20 characters.")]
    public string? PhoneNumber { get; set; }

}
