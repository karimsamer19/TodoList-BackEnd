using System.ComponentModel.DataAnnotations;

namespace TodoList.DTOs;

public class UserDto
{
    [Required]
    [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
    public string username { get; set; }
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string? Eamil { get; set; }
}
