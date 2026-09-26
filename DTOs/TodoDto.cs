using System.ComponentModel.DataAnnotations;

namespace TodoList.DTOs;

public class TodoDto
{

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }
    public string Description { get; set; }

    public bool isCompleted { get; set; } = false;
    public string UserId { get; set; }
}
