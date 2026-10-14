using System.ComponentModel.DataAnnotations;

namespace TodoList.DTOs;

public class UserDto
{
    [Required]
    [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
    public string username { get; set; }
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string? Email { get; set; }
    [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone Number must be exactly 11 digits")]
    public string? PhoneNumber { get; set; }

    public ICollection<TodoDto>? todos { get; set; }
}
