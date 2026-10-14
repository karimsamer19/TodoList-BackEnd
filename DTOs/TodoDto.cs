using System.ComponentModel.DataAnnotations;

namespace TodoList.DTOs;

public class TodoDto
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }
    public string Description { get; set; }

    public bool isCompleted { get; set; } = false;
}
